using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NYSM.Data;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Controllers
{
    [ApiController]
    [Route("appfile/")]
    public class AppFileController : ControllerBase
    {
        private readonly IAppFileRepo _repository;
        private readonly IMapper _mapper;

        public AppFileController(IAppFileRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET appfile/
        [HttpGet]
        public ActionResult <IEnumerable<AppFile>> GetAllAppFiles()
        {
            var allappfiles= _repository.GetAllObjects();
            if(allappfiles == null)
            {
                return NotFound();
            }
            return Ok(allappfiles);
        }

        //GET appfile/{id}
        [HttpGet("{id:int}",Name="GetAppFileById")]
        public ActionResult <AppFile> GetAppFileById(int id)
        {
            var appFile = _repository.GetObjectById(id);
            if(appFile != null)
            {
                return Ok(appFile);
            }
            return NotFound();
        }
        //POST appfile/
        [HttpPost]
        public ActionResult <AppFile> CreateAppFile(AppFileCreateAlterDto objectCreateAlterDto)
        {
            try
            {
                
                var appFileModel = _mapper.Map<AppFile>(objectCreateAlterDto);

                char[] delimiters = new char[] {' ', '\r', '\n','\t' };

                appFileModel.WordCount =objectCreateAlterDto.Content.Split(delimiters,StringSplitOptions.RemoveEmptyEntries).Length;  
                
                appFileModel.LineCount=0;

                _repository.CreateObject(appFileModel);

                _repository.SaveChanges();

                return CreatedAtRoute(/*nome da rota GET*/nameof(GetAppFileById),
                 /*valor para passar no GET*/new {Id = appFileModel.Id},
                 /*body*/objectCreateAlterDto);    
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        //PUT appfile/{id} 
        [HttpPut("{id:int}")]
        public ActionResult UpdateAppFile(int id, AppFileCreateAlterDto objectCreateAlterDto)
        {
            // filtra do banco de dados o registro que esta sendo atualizado
            var appFilefromRepo = _repository.GetObjectById(id);
            if(appFilefromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(objectCreateAlterDto,appFilefromRepo);

            
            try{
                _repository.BeginTransaction();
                //salva no banco de dados o registro alterado
                _repository.SaveChanges();
                
                _repository.Commit();
            }
            catch(Exception)
            {
                _repository.Rollback();
                return BadRequest();
            }
            
            return Ok();
        }
        //DELETE appfile/{id}
        [HttpDelete("{id:int}")]
        public ActionResult DeleteAppFile(int id)
        {
             // filtra do banco de dados o registro que esta sendo atualizado
            var appfile = _repository.GetObjectById(id);
            if(appfile != null)
            {
                //deleta
                _repository.DeleteObject(appfile);
                //efetiva o delete no banco de dados.
                _repository.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        //DELETE appfile/
        [HttpDelete]
        public ActionResult DeleteAppFiles([FromBody] int[] id)
        {
            try
            {
                _repository.BeginTransaction();
                foreach(int nr_id in id)
                {
                    var appfile = _repository.GetObjectById(nr_id);
                    if(appfile != null)
                    {
                        _repository.DeleteObject(appfile);
                    }                
                }
                //efetiva o delete no banco de dados.
                _repository.SaveChanges();
                _repository.Commit();
                return Ok();
            }
            catch(Exception)
            {
                _repository.Rollback();
                return BadRequest();
            }
        }
    }
}