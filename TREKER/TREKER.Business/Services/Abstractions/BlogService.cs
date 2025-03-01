﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.BlogVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogImageRepository _blogImageRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string[] includes =
        {
            "Destination",
            "Images"
        };
        private readonly string[] includes2 =
        {
            "Images"
        };

        public BlogService(IBlogRepository blogRepository, IConfiguration configuration, IBlogImageRepository blogImageRepository)
        {
            _blogRepository = blogRepository;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("AzureContainer");
            _blogImageRepository = blogImageRepository;
        }

        public async Task CreateAsync(CreateBlogVM vm)
        {
            var newBlog = new Blog
            {
                Title = vm.Title,
                Description = vm.Description,
                ByUsername = vm.ByUsername,
                DestinationId = vm.DestinationId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            newBlog.Images = await CreateBlogImagesAsync(vm, newBlog);

            await _blogRepository.CreateAsync(newBlog);
            await _blogRepository.SaveChangesAsync();
        }


        public async Task<List<BlogImage>> CreateBlogImagesAsync(CreateBlogVM vm, Blog newBlog)
        {
            List<BlogImage> blogImages = new List<BlogImage>();

            foreach (var image in vm.Images)
            {
                blogImages.Add(
                    new BlogImage
                    {
                        ImageUrl = await image.UploadFileAsync(_connectionString, "/BlogPictures/"),
                        Blog = newBlog
                    }
                    );
            }

            return blogImages;
        }

        public async Task DeleteAsync(int id)
        {
            await _blogRepository.DeleteAsync(id);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAllAsync(includes: includes);
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id, includes);
        }

        public async Task RecoverAsync(int id)
        {
            await _blogRepository.RecoverAsync(id);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var oldBlog = await _blogRepository.GetByIdAsync(id, includes);

            foreach (var item in oldBlog.Images)
            {
                oldBlog.Images.ToList().Remove(item);

                Uri uri = new Uri(item.ImageUrl);
                string blobName = uri.Segments.Last();
                await FileManager.DeleteFileAsync(blobName, _connectionString, "/BlogPictures/");
            }

            _blogRepository.Remove(id);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateBlogVM vm)
        {
            var oldBlog = await _blogRepository.GetByIdAsync(vm.Id, includes2);

            oldBlog.Title = vm.Title ?? oldBlog.Title;
            oldBlog.Description = vm.Description ?? oldBlog.Description;
            oldBlog.ByUsername = vm.ByUsername ?? oldBlog.ByUsername;
            oldBlog.DestinationId = vm.DestinationId;
            oldBlog.UpdatedDate = DateTime.UtcNow;

            oldBlog.Images = await UpdateBlogImagesAsync(vm, oldBlog);

            await _blogRepository.UpdateAsync(oldBlog);
            await _blogRepository.SaveChangesAsync();
        }
        public async Task<List<BlogImage>> UpdateBlogImagesAsync(UpdateBlogVM vm, Blog oldBlog)
        {
            List<BlogImage> blogImages = oldBlog.Images.ToList();

            if (vm.ViewImageIds == null)
            {
                blogImages.RemoveAll(x => x.BlogId == vm.Id);

                foreach (var item in oldBlog.Images)
                {
                    _blogImageRepository.Remove(item.Id);
                }
            }
            else
            {
                List<BlogImage> removeList = blogImages.Where(pt => !vm.ViewImageIds.Contains(pt.Id) && pt.BlogId == vm.Id).ToList();

                if (removeList.Count > 0)
                {
                    foreach (var item in removeList)
                    {
                        blogImages.Remove(item);

                        _blogImageRepository.Remove(item.Id);

                        Uri uri = new Uri(item.ImageUrl);
                        string blobName = uri.Segments.Last();
                        await FileManager.DeleteFileAsync(blobName, _connectionString, "/BlogPictures/");
                    }
                }
            }

            if (vm.Images is not null)
            {
                foreach (var image in vm.Images)
                {
                    blogImages.Add(
                        new BlogImage
                        {
                            ImageUrl = await image.UploadFileAsync(_connectionString, "/BlogPictures/"),
                            Blog = oldBlog,
                        }
                        );
                }
            }


            return blogImages;
        }
    }
}
