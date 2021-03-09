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
    [Route("readspeed/")]
    public class ReadSpeedController : ControllerBase
    {
        private readonly IReadSpeedRepo _repository;
        private readonly IMapper _mapper;

        public ReadSpeedController(IReadSpeedRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }
        //POST readspeed/
        //notações como HttpPost,HttpDelete("{id}").. etc definem qual método devera ser chamado de acordo com a requisição recebida
        [HttpPost]
        public ActionResult<object> CreateReadSpeed(ReadSpeedDto objectCreateAlterDto)
        {
            try
            {
                var readSpeedModel = _mapper.Map<ReadSpeed>(objectCreateAlterDto);
                _repository.CreateObject(readSpeedModel);

                _repository.SaveChanges();

                return CreatedAtRoute(/*nome da rota GET*/nameof(GetReadSpeedById),
                 /*valor para passar no GET*/new {Id = readSpeedModel.Id},
                 /*body*/objectCreateAlterDto);    
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        //DELETE readspeed/
        [HttpDelete("{id}")]
        public ActionResult DeleteReadSpeed(int id)
        {
             // filtra do banco de dados o registro que esta sendo atualizado
            var readspeed = _repository.GetObjectById(id);
            if(readspeed != null)
            {
                //deleta
                _repository.DeleteObject(readspeed);
                //efetiva o delete no banco de dados.
                _repository.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        //DELETE readspeed/   -Para deletar multiplos valores insira no corpo da requisicao delete  = [ 4,6,55 ]
        [HttpDelete]
        public ActionResult DeleteReadSpeeds([FromBody] int[] id)
        {
            try
            {
                _repository.BeginTransaction();
                foreach(int nr_id in id)
                {
                    var readspeed = _repository.GetObjectById(nr_id);
                    if(readspeed != null)
                    {
                        _repository.DeleteObject(readspeed);
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

        //GET readspeed/
        [HttpGet]
        public ActionResult<IEnumerable<ReadSpeed>> GetReadSpeeds()
        {
            var allreadspeed= _repository.GetAllObjects();
            if(allreadspeed == null)
            {
                return NotFound();
            }
            return Ok(allreadspeed);
        }

        //GET readspeed/{id}
        [HttpGet("{id:int}",Name="GetReadSpeedById")]
        public ActionResult<ReadSpeed> GetReadSpeedById(int id)
        {
            var readspeed = _repository.GetObjectById(id);
            if(readspeed != null)
            {
                return Ok(readspeed);
            }
            return NotFound();
        }
        //PUT readspeed/{id}
        [HttpPut("{id:int}")]
        public ActionResult UpdateReadSpeed(int id, ReadSpeedDto objectCreateAlterDto)
        {
            // filtra do banco de dados o registro que esta sendo atualizado
            var readspeedfromRepo = _repository.GetObjectById(id);
            if(readspeedfromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(objectCreateAlterDto,readspeedfromRepo);

            
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
    }
}