using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class BookTests : IDisposable
    {
        private RestClient client;
        private string token;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("john.doe@example.com", "password123");

            Assert.That(token, Is.Not.Null.Or.Empty, "Authentication token should not be null or empty");
        }

        [Test]
        public void Test_GetAllBooks()
        {
            //Arrange
            var getRequestAllBooks = new RestRequest("book", Method.Get);

            //Act
            var getResponseAllBooks = client.Execute(getRequestAllBooks);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(getResponseAllBooks.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code is not ok");

                Assert.That(getResponseAllBooks.Content, Is.Not.Null.Or.Empty, "Response content is null or empty");

                var books = JArray.Parse(getResponseAllBooks.Content);

                Assert.That(books.Type, Is.EqualTo(JTokenType.Array), "The response content is not JSON Array");

                Assert.That(books.Count, Is.GreaterThan(0), "Expected books are less than one");

                foreach (var book in books) 
                {
                    Assert.That(book["title"]?.ToString(), Is.Not.Null.Or.Empty, "Property title is not as expected");

                    Assert.That(book["author"]?.ToString(), Is.Not.Null.Or.Empty, "Property author is not as expected");

                    Assert.That(book["description"]?.ToString(), Is.Not.Null.Or.Empty, "Property description is not as expected");

                    Assert.That(book["price"]?.ToString(), Is.Not.Null.Or.Empty, "Property price is not as expected");

                    Assert.That(book["pages"]?.ToString(), Is.Not.Null.Or.Empty, "Property pages is not as expected");

                    Assert.That(book["category"]?.ToString(), Is.Not.Null.Or.Empty, "Property category is not as expected");

                }
            });
         
        }

        [Test]
        public void Test_GetBookByTitle()
        {
            //Arrange
            var getRequestAllBooks = new RestRequest("book", Method.Get);

            //Act
            var getResponseAllBooks = client.Execute(getRequestAllBooks);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(getResponseAllBooks.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

                Assert.That(getResponseAllBooks.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

                var books = JArray.Parse(getResponseAllBooks.Content);
                var book = books.FirstOrDefault(b => b["title"]?.ToString() == "The Great Gatsby");

                Assert.That(book["author"]?.ToString(), Is.EqualTo("F. Scott Fitzgerald"), "Author property does not have the correct value");
            });
        }

        [Test]
        public void Test_AddBook()
        {
            //Arrange
            //Get all categories and extract first category id

            var getCategoriesRequest = new RestRequest("category", Method.Get);

            var getCategoriesResponse = client.Execute(getCategoriesRequest);

            var categories = JArray.Parse(getCategoriesResponse.Content);
            var firstCategory = categories.First();
            var categoryId = firstCategory["_id"]?.ToString();

            //Add new book
            var addRequest = new RestRequest("book", Method.Post);
            addRequest.AddHeader("Authorization", $"Bearer {token}");
            var title = "Of Mice and Men";
            var author = "John Steinbeck";
            var description = "Describes the experiences of two displaced migrant ranch workers, as they move from place to place in California, searching for jobs during the Great Depression";
            var price = 20;
            var pages = 150;
            addRequest.AddJsonBody(new { 
                title, 
                author, 
                description, 
                price, 
                pages,
                category = categoryId
            });

            //Act
            var addResponse = client.Execute(addRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(addResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

                Assert.That(addResponse.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

                var createdBook = JObject.Parse(addResponse.Content);
                Assert.That(createdBook["_id"]?.ToString(), Is.Not.Empty);

                var createdBookId = createdBook["_id"]?.ToString();

                //Get book by id
                var getBookRequest = new RestRequest($"/book/{createdBookId}", Method.Get);

                var getResponse = client.Execute(getBookRequest);

                Assert.Multiple(() =>
                {
                    Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

                    Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

                    var book = JObject.Parse(getResponse.Content);

                    Assert.That(book["title"]?.ToString(), Is.EqualTo(title));
                    Assert.That(book["author"]?.ToString(), Is.EqualTo(author));
                    Assert.That(book["price"]?.Value<int>(), Is.EqualTo(price));
                    Assert.That(book["pages"]?.Value<int>(), Is.EqualTo(pages));

                    Assert.That(book["category"]?.ToString(), Is.Not.Null.Or.Empty);

                    Assert.That(book["category"]["_id"]?.ToString(), Is.EqualTo(categoryId));
                });
            });
        }

        [Test]
        public void Test_UpdateBook()
        {
            //Arrange
            //Get all books and extract with title "The Catcher in the Rye"

            var getRequest = new RestRequest("book", Method.Get);
            var getResponse = client.Execute(getRequest);

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

            Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

            var books = JArray.Parse(getResponse.Content);
            var bookToUpdate = books.FirstOrDefault(b => b["title"]?.ToString() == "The Catcher in the Rye");

            Assert.That(bookToUpdate, Is.Not.Null);

            var bookId = bookToUpdate["_id"]?.ToString();

            //Create update request
            var updateRequest = new RestRequest($"book/{bookId}", Method.Put);

            updateRequest.AddHeader("Authorization", $"Bearer {token}");
            updateRequest.AddJsonBody(new
            {
                title = "Updated Book Title",
                author = "Updated Author"
            });

            //Act
            var updateResponse = client.Execute(updateRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

                Assert.That(updateResponse.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

                var updatedBook = JObject.Parse(updateResponse.Content);

                Assert.That(updatedBook["title"]?.ToString(), Is.EqualTo("Updated Book Title"));
                Assert.That(updatedBook["author"]?.ToString(), Is.EqualTo("Updated Author"));
            });
            
        }

        [Test]
        public void Test_DeleteBook()
        {
            //Arrange
            //Get all books and extract with name "To Kill a Mockingbird" 
            var getRequest = new RestRequest("book", Method.Get);
            var getResponse = client.Execute(getRequest);

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

            Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is not as expected");

            var books = JArray.Parse(getResponse.Content);
            var bookToDelete = books.FirstOrDefault(b => b["title"]?.ToString() == "To Kill a Mockingbird");

            Assert.That(bookToDelete, Is.Not.Null);

            var bookId = bookToDelete["_id"]?.ToString();

            //Create delete request
            var deleteRequest = new RestRequest($"book/{bookId}", Method.Delete);

            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            //Act
            var deleteResponse = client.Execute(deleteRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");

                //Get request to get the deleted book
                var verifyRequest = new RestRequest($"book/{bookId}");

                var verifyResponse = client.Execute(verifyRequest);

                Assert.That(verifyResponse.Content, Is.EqualTo("null"));
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
