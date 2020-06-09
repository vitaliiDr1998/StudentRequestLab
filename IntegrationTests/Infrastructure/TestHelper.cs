using BLL.Core.DTO;
using DAL.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    public class TestHelper
    {
        public static List<HtmlNode> GetTables(string response)
        {
            var source = WebUtility.HtmlDecode(response);
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(response);
            return resultat.DocumentNode.Descendants().Where(x => x.Name == "table").ToList();
        }

        public static List<StudentDto> GetStudents(string response)
        {
            var studentList = new List<StudentDto>();
            var studentTable = GetTables(response).First(x => x.Id == "students");
            var rows = studentTable.Descendants().Where(x => !string.IsNullOrEmpty(x.Id))?.ToList();
            foreach (var row in rows)
            {
                var tds = row.Descendants().Where(x => x.Name == "td").ToList();
                studentList.Add(new StudentDto
                {
                    Id = int.Parse(tds[0].InnerText.Trim()),
                    Name = tds[1].InnerText.Trim(),
                    Surname = tds[2].InnerText.Trim(),
                    Priority = int.Parse(tds[3].InnerText.Trim())
                });
            }
            return studentList;
        }

        public static List<LectorDto> GetLectors(string response)
        {
            var lectorList = new List<LectorDto>();
            var lectorTable = GetTables(response).First(x => x.Id == "lectors");
            var rows = lectorTable.Descendants().Where(x => !string.IsNullOrEmpty(x.Id))?.ToList();
            foreach (var row in rows)
            {
                var tds = row.Descendants().Where(x => x.Name == "td").ToList();
                lectorList.Add(new LectorDto
                {
                    Id = int.Parse(tds[0].InnerText.Trim()),
                    Name = tds[1].InnerText.Trim(),
                    Surname = tds[2].InnerText.Trim(),
                });
            }
            return lectorList;
        }
    }
}
