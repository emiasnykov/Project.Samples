using System;
using System.Net;
using Newtonsoft.Json;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.QA_Enums;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    public class JSONValidate
    {
        #region Public methods
        /// **************************************
        
        /// <summary>
        /// Validate if JSON content is parseable
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_JSONContent">As itself</param>
        /// <returns>Return result JSON if parseable</returns>
        public static string ValidateIfJSONContentIsParseable(ApiUri _ApiUri, string _JSONContent)
        {
            string resJSONContent = null;

            try
            {
                // Deserialize object by JsonCOnvert
                resJSONContent = JsonConvert.DeserializeObject(_JSONContent).ToString();
            }
            catch (Exception)
            {
                // Mark stright away what is failed
                WebApiUri.MarkFailingPath(_ApiUri);

                // TODO
                TestsExceptions.ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.OnAttachedJSONParse);
            }

            return resJSONContent;
        }

        /// <summary>
        /// Validate if template schema item is defined
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_SchemaTemplateItem">Schema template item</param>
        public static void ValidateSchemaTemplateItemWithSchemaJSONTemplate(ApiUri _ApiUri, string _SchemaTemplateItem)
        {
            // Throw exception if no maching schema JSON template and missing Value
            if (!FactoryParam.SchemaJSONTemplate.Contains(_SchemaTemplateItem))
            {
                // Mark stright away what is failed
                WebApiUri.MarkFailingPath(_ApiUri);

                // TODO
                TestsExceptions.ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.OnUnknownSchemaJSONParsingTemplate, HttpStatusCode.Unused, new Exception($"New SchemaTepmlateItem = '{_SchemaTemplateItem}'"));
            }
        }

        #endregion Public methods
    }
}
