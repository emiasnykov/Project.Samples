using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Factory;
using ResWebApiTest.TestEngine.Helpers;
using ResWebApiTest.TestEngine.Managers;
using ResWebApiTest.TestEngine.QA_Enums;
using ResWebApiTest.TestEngine.QA_Structs;
using static ResWebApiTest.TestEngine.Constants.BasicEntity;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.QA_InternalTools
{
    /// <summary>
    /// Asserts generator used in WebApi tests
    /// </summary>
    public class QA_AssertsGenerator
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Generates automatically Asserts, where the invocation took place
        /// </summary>
        public static void GenerateAsserts()
        {
            // Set proper IO data
            string testClass = WebApiTestManager.TestClass; 
            string innerTestDirectory = $"{testClass.Replace(TestToolsParam.TestApiDirectory, "")}\\";
            string testClassDirectory = $"{Configurer.GetWebApiTestSourceTestsFolder()}{innerTestDirectory}{testClass}.{TestToolsParam.TestFileExt}";
            string testMethod = WebApiTestManager.TestMethod; 

            // Set auto-generated hedaer
            string res = $"{TestToolsParam.TestRegion}{testMethod}" +
                         $"{NewLine}" +
                         $"{NewLine}" +
                         $"{IndentTestRegion}//{TestToolsParam.TestAsserts}";

            // Iterate through the JSON dictionary
            IList<string> assertLst = null;
            IList<string> generatedLst = IterateJSONObject(ApiUri.Request, JSONParser.SchemaJSON, QA_IterationTypeMode.AssertGeneration);

            // Set the value to make sure the last counting validation is passed after asserts generation
            WebApiTestManager.ResponseCount = generatedLst.Count;

            // Prepare output of Asserts' auto-generation
            foreach (string itm in generatedLst)
            {
                assertLst = new List<string>();
                assertLst = itm.Split('|').ToList();
                string memberPath = ((assertLst[0].ToString().Equals("0")) ? "Result" : assertLst[0]);

                // Rules
                if ((assertLst[2].Equals("DateTime") && (
                    (memberPath.ToUpper().Contains("CREAT") && !memberPath.ToUpper().Contains("BY"))
                    ||
                    (memberPath.ToUpper().Contains("MODIF") && !memberPath.ToUpper().Contains("BY"))
                    ||
                    (memberPath.ToUpper().Contains("MINDATE") || memberPath.ToUpper().Contains("MAXDATE"))
                    )))
                    res += $"{NewLine}" +
                           $"{IndentTestRegion}" +
                           $"Assert.GreaterOrEqual(WebApiTest.Node<{assertLst[2]}>({memberPath}), {assertLst[1]});";
                else
                if (memberPath.ToUpper().Contains("IMAGE"))
                {
                    string val = assertLst[1];
                    if (val == null || val.Equals ("\"\""))
                        res += $"{NewLine}" +
                               $"{IndentTestRegion}Assert.IsEmpty(WebApiTest.Node<{assertLst[2]}>({memberPath}));";
                    else
                        res += $"{NewLine}" +
                               $"{IndentTestRegion}Assert.IsNotEmpty(WebApiTest.Node<{assertLst[2]}>({memberPath}));";
                }else
                if (memberPath.ToUpper().Contains("PHONE"))
                {
                    string val = assertLst[1];
                    if (val == null || val.Equals("\"\""))
                        res += $"{NewLine}" +
                               $"{IndentTestRegion}Assert.AreEqual(\"\",WebApiTest.Node<string>({memberPath}));";
                    else
                        res += $"{NewLine}" +
                               $"{IndentTestRegion}Assert.AreEqual(\"{val}\",WebApiTest.Node<string>({memberPath}));";
                }
                else
                if (assertLst[2].Equals("string"))
                    res += $"{NewLine}" +
                           $"{IndentTestRegion}Assert.AreEqual({assertLst[1]}, WebApiTest.Node({memberPath}));";
                else
                if (assertLst[2].Equals("object"))
                    res += $"{NewLine}" +
                           $"{IndentTestRegion}Assert.IsEmpty(WebApiTest.Node({memberPath})); //JSON result array is empty";
                else
                    res += $"{NewLine}" +
                           $"{IndentTestRegion}Assert.AreEqual({assertLst[1]}, WebApiTest.Node<{assertLst[2]}>({memberPath}));";
            }

            res += $"{NewLine}{NewLine}{IndentTestRegion}#endregion Asserts for {testMethod}";

            // Ouput IO
            String TestClassContent;
            using (StreamReader sr = new StreamReader(testClassDirectory))
            {
                TestClassContent = sr.ReadToEnd();
                sr.Close();
            }
            TestClassContent = TestClassContent.Replace(TestToolsParam.QA_GenerateAsserts, res);
            Encoding utf8 = Encoding.UTF8;
            using (FileStream fs = new FileStream(testClassDirectory, FileMode.Truncate, FileAccess.Write))
            {
                fs.Write(utf8.GetBytes(TestClassContent), 0, utf8.GetByteCount(TestClassContent));
                fs.Close();
            }
        }

        /// <summary>
        /// Iterate through all response JSON and get all values / Create item schema
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Object name with its value</returns>
        public static IList<string> IterateJSONObject(ApiUri _ApiUri, IList<string> _SchemaJSONItemList, QA_IterationTypeMode _IterationType = QA_IterationTypeMode.Information)
        {
            IList<String> schemaJSONwrk = _SchemaJSONItemList; // SchemaJSONObjectList;
            IList<string> elSchemaLst = null;
            IList<string> resResponseJson = new List<string>();
            Dictionary<string, object> responseJSON = null;

            // Get proper response. By this time the response should exist, nevertheles if somehow not found response, throw exception
            responseJSON = WebApiUri.GetPathResponse(_ApiUri);
            if (responseJSON == null)
                TestsExceptions.ThrowExceptionOnFailure (_ApiUri, QA_ServeExceptionMode.OnStatusCode, HttpStatusCode.NotFound);

            // Iterate through SchemaJSONObjectList
            foreach (string elSchemaArr in _SchemaJSONItemList)
            {
                string schemaTemplItm = "";
                List<string> schemaItmLst = new List<string>();
                elSchemaLst = new List<string>();
                elSchemaLst = elSchemaArr.Split(',').ToList();

                // Iterate through schema elements
                foreach (string elSchema in elSchemaLst)
                {
                    schemaTemplItm += elSchema.Substring(0, 1);
                    schemaItmLst.Add(elSchema.Substring(1, elSchema.Length - 1));
                }

                // Throw exception if no maching schema JSON template and missing Value
                JSONValidate.ValidateSchemaTemplateItemWithSchemaJSONTemplate(_ApiUri, schemaTemplItm);

                // Add value
                string obj = null;
                object val = null;
                string res = null;
                switch (schemaTemplItm)
                {
                    case "N":            // No List / No nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"0";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"0\"";
                                break;
                        }
                        val = JSONRepository.Value("0", responseJSON, false);
                        break;
                    case "D":            // No List / First nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], responseJSON, false);
                        break;
                    case "DD":           // No List / First nesting - No item list / Second nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}.{schemaItmLst[1]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", \"{schemaItmLst[1]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], schemaItmLst[1], responseJSON, false);
                        break;
                    case "DI":           // No List / First nesting with array 
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}]";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), responseJSON, false);
                        break;
                    case "DDD":          // No List / First nesting - No item list / Second nesting - No item list / Third nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}.{schemaItmLst[1]}.{schemaItmLst[2]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", \"{schemaItmLst[1]}\", \"{schemaItmLst[2]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], schemaItmLst[1], schemaItmLst[2], responseJSON, false);
                        break;
                    case "DDAD":        // No List / First nesting - No item list / Second nesting - With item list / Third nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}.{schemaItmLst[1]}[{schemaItmLst[2]}].{schemaItmLst[3]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", \"{schemaItmLst[1]}\", {schemaItmLst[2]}, \"{schemaItmLst[3]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), schemaItmLst[3], responseJSON, false);
                        break;
                    case "DAD":          // No List / First nesting - With item list / Second nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], responseJSON, false);
                        break;
                    case "DADI":         // No List / First nesting - With item list / Second nesting with array
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}[{schemaItmLst[3]}]";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\", {schemaItmLst[3]}";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], Convert.ToInt32(schemaItmLst[3]), responseJSON, false);
                        break;
                    case "DADD":        // No List / First nesting - With item list / Second nesting - No item list / Third nesting - No item list    
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}.{schemaItmLst[3]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\", \"{schemaItmLst[3]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], schemaItmLst[3], responseJSON, false);
                        break;
                    case "DADAD":         // No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}[{schemaItmLst[3]}].{schemaItmLst[4]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\", {schemaItmLst[3]}, \"{schemaItmLst[4]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], Convert.ToInt32(schemaItmLst[3]), schemaItmLst[4], responseJSON, false);
                        break;
                    case "DADADI":         // No List / First nesting - With item list / Second nesting - with item list / Third nesting with array
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}[{schemaItmLst[3]}].{schemaItmLst[4]}[{schemaItmLst[5]}]";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\", {schemaItmLst[3]}, \"{schemaItmLst[4]}\", {schemaItmLst[5]}";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], Convert.ToInt32(schemaItmLst[3]), schemaItmLst[4], Convert.ToInt32(schemaItmLst[5]), responseJSON, false);
                        break;
                    case "DADADAD":         // No List / First nesting - With item list / Second nesting - with item list / Third nesting - with item list / Fourth nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"{schemaItmLst[0]}[{schemaItmLst[1]}].{schemaItmLst[2]}[{schemaItmLst[3]}].{schemaItmLst[4]}[{schemaItmLst[5]}].{schemaItmLst[6]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"\"{schemaItmLst[0]}\", {schemaItmLst[1]}, \"{schemaItmLst[2]}\", {schemaItmLst[3]}, \"{schemaItmLst[4]}\", {schemaItmLst[5]}, \"{schemaItmLst[6]}\"";
                                break;
                        }
                        val = JSONRepository.Value(schemaItmLst[0], Convert.ToInt32(schemaItmLst[1]), schemaItmLst[2], Convert.ToInt32(schemaItmLst[3]), schemaItmLst[4], Convert.ToInt32(schemaItmLst[5]), schemaItmLst[6], responseJSON, false);
                        break;
                    case "AD":           // Big list / First nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], responseJSON, false);
                        break;
                    case "ADD":          // Big list / First nesting - No item list / Second nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}.{schemaItmLst[2]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", \"{schemaItmLst[2]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], schemaItmLst[2], responseJSON, false);
                        break;
                    case "ADI":          // Big list / First nesting with array
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}[{schemaItmLst[2]}]";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", {schemaItmLst[2]}";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), responseJSON, false);
                        break;
                    case "ADAD":         // Big list / First nesting - With item list / Second nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}[{schemaItmLst[2]}]{schemaItmLst[3]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", {schemaItmLst[2]}, \"{schemaItmLst[3]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), schemaItmLst[3], responseJSON, false);
                        break;
                    case "ADADI":        // Big list / First nesting - With item list / Second nesting with array
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}[{schemaItmLst[2]}]{schemaItmLst[3]}[{schemaItmLst[4]}]";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", {schemaItmLst[2]}, \"{schemaItmLst[3]}\", \"{schemaItmLst[4]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), schemaItmLst[3], responseJSON, false);
                        break;
                    case "ADADAD":        // Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}[{schemaItmLst[2]}]{schemaItmLst[3]}[{schemaItmLst[4]}]{schemaItmLst[5]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", {schemaItmLst[2]}, \"{schemaItmLst[3]}\", {schemaItmLst[4]}, \"{schemaItmLst[5]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), schemaItmLst[3], Convert.ToInt32(schemaItmLst[4]), schemaItmLst[5], responseJSON, false);
                        break;
                    case "ADADADD":     // Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
                        switch (_IterationType)
                        {
                            case QA_IterationTypeMode.Information:
                                obj = $"[{schemaItmLst[0]}]{schemaItmLst[1]}[{schemaItmLst[2]}]{schemaItmLst[3]}[{schemaItmLst[4]}]{schemaItmLst[5]}{schemaItmLst[6]}";
                                break;
                            case QA_IterationTypeMode.AssertGeneration:
                                obj = $"0, {schemaItmLst[0]}, \"{schemaItmLst[1]}\", {schemaItmLst[2]}, \"{schemaItmLst[3]}\", {schemaItmLst[4]}, \"{schemaItmLst[5]}\", \"{schemaItmLst[6]}\"";
                                break;
                        }
                        val = JSONRepository.Value(0, Convert.ToInt32(schemaItmLst[0]), schemaItmLst[1], Convert.ToInt32(schemaItmLst[2]), schemaItmLst[3], Convert.ToInt32(schemaItmLst[4]), schemaItmLst[5], schemaItmLst[6], responseJSON, false);
                        break;
                    default:
                        break;
                }

                // Format result
                QA_CastObjectStruct objCast = GetCastType(val);
                switch (_IterationType)
                {
                    case QA_IterationTypeMode.Information:
                        res = $"{obj}|: {Convert.ToString(val)}";
                        break;
                    case QA_IterationTypeMode.AssertGeneration:
                        if (objCast.type.Equals("string"))
                            res = $"{obj}|\"{objCast.value}\"|{objCast.type}";
                        else
                            res = $"{obj}|{objCast.value}|{objCast.type}";
                        break;
                }

                // Add result
                resResponseJson.Add(res);
            }

            return resResponseJson;
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        /// <summary>
        /// Get cast type of passing object
        /// </summary>
        /// <param name="_ObjToCast">Passed object to get its type</param>
        /// <returns>cast type structure</returns>
        private static QA_CastObjectStruct GetCastType(object _ObjToCast)
        {
            bool isParsed = false;
            QA_CastObjectStruct resCastObj = new QA_CastObjectStruct() { type = null, value = null };

            // Check if note null
            if (_ObjToCast != null)
            {
                // Is it Datetime
                if (!isParsed)
                {
                    string[] formats = {"yyyy-MM-ddTHH:mm:ss", 
                                        "yyyy-MM-ddTHH:mm:ss.f", 
                                        "yyyy-MM-ddTHH:mm:ss.ff", 
                                        "yyyy-MM-ddTHH:mm:ss.fff",
                                        "yyyy-MM-ddTHH:mm:ss.ffff", 
                                        "yyyy-MM-ddTHH:mm:ss.fffff", 
                                        "yyyy-MM-ddTHH:mm:ss.ffffff", 
                                        "yyyy-MM-ddTHH:mm:ss.ffffffK",
                                        "yyyy-MM-ddTHH:mm:ss.fffffff", 
                                        "yyyy-MM-ddTHH:mm:ss.fffffffK", 
                                        "yyyy-MM-ddTHH:mm:ss.fffffffz",
                                        "yyyy-MM-ddTHH:mm:ss.fffffffzz", 
                                        "yyyy-MM-ddTHH:mm:ss.fffffffzzz"
                                        };
                    if (DateTime.TryParseExact(_ObjToCast.ToString(), formats, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime parse))
                    {
                        if ((parse.Hour.Equals(0)) && parse.Minute.Equals(0) && parse.Second.Equals(0))
                            resCastObj = new QA_CastObjectStruct() { type = "DateTime", value = $"new DateTime({parse.Year}, {parse.Month}, {parse.Day})" };
                        else
                            resCastObj = new QA_CastObjectStruct() { type = "DateTime", value = $"new DateTime({parse.Year}, {parse.Month}, {parse.Day}, {parse.Hour}, {parse.Minute}, {parse.Second}, {parse.Millisecond})" };
                        isParsed = !isParsed;
                    }
                }
                // Is it int
                if (!isParsed)
                {
                    if (Int32.TryParse(_ObjToCast.ToString(), out int parse))
                    {
                        resCastObj = new QA_CastObjectStruct() { type = "int", value = parse.ToString() };
                        isParsed = !isParsed;
                    }
                }
                // Is it double
                if (!isParsed)
                {
                    if (Double.TryParse(_ObjToCast.ToString(), out double parse))
                    {
                        resCastObj = new QA_CastObjectStruct() { type = "double", value = parse.ToString() };
                        isParsed = !isParsed;
                    }
                }
                // Is it boolean
                if (!isParsed)
                {
                    if (Boolean.TryParse(_ObjToCast.ToString(), out bool parse))
                    {
                        resCastObj = new QA_CastObjectStruct() { type = "bool", value = parse.ToString().ToLower() };
                        isParsed = !isParsed;
                    }
                }
                // Finally string
                if (!isParsed)
                {
                    if (_ObjToCast.ToString().Length == 0)
                        resCastObj = new QA_CastObjectStruct() { type = "string", value = "" };
                    else
                        resCastObj = new QA_CastObjectStruct() { type = "string", value = _ObjToCast.ToString() };
                }
            }
            else
                resCastObj = new QA_CastObjectStruct() { type = "object", value = null };

            return resCastObj;
        }

        #endregion Private methods
    }
}
