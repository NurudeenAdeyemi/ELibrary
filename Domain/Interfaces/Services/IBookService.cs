﻿using Domain.Enums;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IBookService 
    {
        public Task<BaseResponse> AddBook(CreateBookRequestModel model);

        public Task<BaseResponse> UpdateBook(int id, UpdateBookRequestModel model);

        public Task<BaseResponse> DeleteBook(int id);

        public Task<BookResponseModel> GetBook(int id);

        public Task<BookResponseModel> GetBookByTitle(string title);

        public Task<BooksResponseModel> GetBooksByAuthor(int authorId);

        public Task<BooksResponseModel> GetBooksByCategory(int categoryId);

        public Task<BooksResponseModel> GetBooksByPublisher(string publisher);

        public Task<BooksResponseModel> GetBooksByPublicationDate(DateTime publicationDate);

        public Task<BooksResponseModel> GetBooksByAvailabilityStatus(BookAvailabilityStatus availabilityStatus);

        public Task<BooksResponseModel> GetBooksByAccessibilityStatus(BookAccessibilityStatus accessibilityStatus);

        public Task<BooksResponseModel> GetBooks();
    }
}