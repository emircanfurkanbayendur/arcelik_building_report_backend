using Moq;
using NUnit.Framework;
using BuildingReport.API.Controllers;
using BuildingReport.DTO;
using BuildingReport.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace BuildingReport.UnitTests

{
    public class DocumentTests
    {
        private Mock<IDocumentService> _documentServiceMock;
        private DocumentController _documentController;

        [SetUp]
        public void Setup()
        {
            _documentServiceMock = new Mock<IDocumentService>();
            _documentController = new DocumentController(_documentServiceMock.Object);

        }

        [Test]
        public void CreateDocument_WithNullDocument_ReturnsBadRequest()
        {
            //arrange
            DocumentDTO DocumentDTO = null;


            //action
            IActionResult result = _documentController.Post(DocumentDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateDocument_WithExistingDocument_ReturnsUnprocessableContent()
        {
            //arrange

            DocumentDTO DocumentDTO = new DocumentDTO()
            {
                Report = Encoding.UTF8.GetBytes("Test"),
            };

            _documentServiceMock.Setup(i => i.DocumentExists(Encoding.UTF8.GetBytes("Test"))).Returns(true);

            //action
            IActionResult result = _documentController.Post(DocumentDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateDocument_WithNewDocument_ReturnsOk()
        {
            //arrange

            DocumentDTO DocumentDTO = new DocumentDTO()
            {
               Report = Encoding.UTF8.GetBytes("Test"),
            };

            _documentServiceMock.Setup(i => i.DocumentExists(Encoding.UTF8.GetBytes("Test"))).Returns(false);

            //action
            IActionResult result = _documentController.Post(DocumentDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }


        [Test]
        public void GetDocuments_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _documentController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _documentController.GetDocuments();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetDocuments_WithValidModelState_ReturnsOK()
        {
            //arrange


            //action
            IActionResult result = _documentController.GetDocuments();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetDocumentsWithID_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _documentController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _documentController.GetDocuments();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetDocumentsWithID_WithValidModelState_ReturnsOK()
        {
            //arrange
            Document document = new Document()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };

            _documentServiceMock.Setup(i => i.GetDocumentById(1)).Returns(document);

            //action
            IActionResult result = _documentController.GetDocuments();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateDocument_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            DocumentDTO documentDTO = new DocumentDTO()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };
            _documentController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _documentController.Put(documentDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void UpdateDocument_WithValidModelState_ReturnsOK()
        {
            //arrange
            DocumentDTO documentDTO = new DocumentDTO()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };

            Document document = new Document()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };


            _documentServiceMock.Setup(i => i.UpdateDocument(document)).Returns(document);

            //action
            IActionResult result = _documentController.Put(documentDTO);



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void Delete_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            Document document = new Document()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };
            _documentController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _documentController.Delete(1);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeleteDocument_WithValidModelState_ReturnsOK()
        {
            //arrange
            Document document = new Document()
            {
                Id = 1,
                Report = Encoding.UTF8.GetBytes("Test"),

            };



            //action
            IActionResult result = _documentController.Delete(document.Id);



            //assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

    }
}