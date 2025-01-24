using System;

namespace ResWebApiTest.Tests.Api.Visit
{
    public static class VisitHelpers
    {
        /// <summary>
        /// Visitor model
        /// </summary>
        public class VisitorData
        {
            public int Id { get; set; }
            public int FolioId { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string DocumentType { get; set; }
            public string DocumentNumber { get; set; }
            public string DocumentForename { get; set; }
            public string DocumentSurname { get; set; }
            public string DocumentAddress { get; set; }
            public DateTime DocumentValidDate { get; set; }
        }
    }
}
