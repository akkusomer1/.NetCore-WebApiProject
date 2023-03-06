using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    public class CustomResponseDto<TEntity>
    {
        public  TEntity Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }


        public static CustomResponseDto<TEntity> Success(TEntity data, int statusCode)
        {
            return new CustomResponseDto<TEntity> { Data = data, StatusCode = statusCode };
        }    
        
        public static CustomResponseDto<TEntity> Success( int statusCode)
        {
            return new CustomResponseDto<TEntity> { StatusCode = statusCode };
        }

        public static CustomResponseDto<TEntity> Fail( int statusCode, List<string> errors)
        {
            return new CustomResponseDto<TEntity> { StatusCode = statusCode, Errors = errors };
        } 
        
        public static CustomResponseDto<TEntity> Fail(TEntity data, int statusCode, string error)
        {
            return new CustomResponseDto<TEntity> { StatusCode = statusCode,Errors=new List<string>() {error } };
        }

    }
}
