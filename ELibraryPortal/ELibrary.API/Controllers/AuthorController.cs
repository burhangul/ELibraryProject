﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ELibrary.API.Base;
using ELibrary.API.Models;
using ELibrary.API.Type;
using ELibrary.DAL.Abstract;
using ELibrary.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.API.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : APIControllerBase
    {
        private readonly IAuthor _author;
        private readonly IBooks _book;

        private IMapper _mapper;

        public AuthorController(IAuthor author, IMapper mapper, IBooks book)
        {
            _author = author;
            _mapper = mapper;
            _book = book;
        }

        [HttpGet]
        [Route("List")]
        public Response<List<AuthorModel>> Get()
        {
            Response<List<AuthorModel>> authorResponse = new Response<List<AuthorModel>>();
            List<Author> entityList = _author.GetList(x => x.IsActive == true);
            authorResponse.Value = _mapper.Map<List<AuthorModel>>(entityList);

            return authorResponse;
        }

        [HttpGet]
        [Route("Detail/{id}")]
        public Response<AuthorModel> AuthorDetail(Guid id)
        {
            Response<AuthorModel> authorResponse = new Response<AuthorModel>();
            Author entity = _author.GetT(x => x.Id==id);
            authorResponse.Value = _mapper.Map<AuthorModel>(entity);

            return authorResponse;
        }


        [HttpGet]
        [Route("Books/{id}")]
        public Response<List<BookModel>> AuthorBooks(Guid id)
        {

            Response<List<BookModel>> bookResponse = new Response<List<BookModel>>();
            List<Book> entityList =  _book.GetList(x => x.AuthorId == id);
            bookResponse.Value = _mapper.Map<List<BookModel>>(entityList.OrderByDescending(f => f.CreatedDate)).ToList();

            return bookResponse;
        }


        [HttpPost]
        [Route("Save")]
        public async Task<Response<AuthorModel>> Post([FromBody]AuthorModel model)
        {
            Response<AuthorModel> authorResponseModel = new Response<AuthorModel>();
            try
            {
                Author entity = _mapper.Map<Author>(model);
                entity = await (model.Id != Guid.Empty ? _author.UpdateAsync(entity) : _author.AddAsync(entity));
                authorResponseModel.Value = _mapper.Map<AuthorModel>(entity);
                authorResponseModel.IsSuccess = true;
            }
            catch (Exception e)
            {
                authorResponseModel.Exception = e;
                authorResponseModel.IsSuccess = false;
            }

            return authorResponseModel;
        }

        [HttpGet]
        [Route("GetOne/{id}")]
        public Response<AuthorModel> GetOne(Guid id)
        {
            Response<AuthorModel> authorResponse = new Response<AuthorModel>();
            Author entityList = _author.GetT(x => x.Id == id);
            authorResponse.Value = _mapper.Map<AuthorModel>(entityList);

            return authorResponse;
        }


    }
}