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
    [Route("question/")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _repository;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //POST question/
        [HttpPost]
        public ActionResult<Question> CreateQuestion(Question objectCreateAlterDto)
        {
            try
            {
                Question questionModel = _mapper.Map<Question>(objectCreateAlterDto);

                _repository.CreateObject(questionModel);

                _repository.SaveChanges();

                return CreatedAtRoute(/*nome da rota GET*/nameof(GetQuestionById),
                 /*valor para passar no GET*/new {Id = questionModel.Id},
                 /*body*/objectCreateAlterDto);    
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        //DELETE question/{id}
        [HttpDelete("{id:int}")]
        public ActionResult DeleteQuestion(int id)
        {
             // filtra do banco de dados o registro que esta sendo atualizado
            var question = _repository.GetObjectById(id);
            if(question != null)
            {
                //deleta
                _repository.DeleteObject(question);
                //efetiva o delete no banco de dados.
                _repository.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        //DELETE question/{id}
        [HttpDelete]
        public ActionResult DeleteQuestion([FromBody] int[] id)
        {
            try
            {
                _repository.BeginTransaction();
                foreach(int nr_id in id)
                {
                    var question = _repository.GetObjectById(nr_id);
                    if(question != null)
                    {
                        _repository.DeleteObject(question);
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

        //GET question/
        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetAllQuestions()
        {
            var allquestions= _repository.GetAllObjects();
            if(allquestions == null)
            {
                return NotFound();
            }
            return Ok(allquestions);
        }

        //GET question/{id}
        [HttpGet("{id:int}", Name="GetQuestionById")]
        public ActionResult<Question> GetQuestionById(int id)
        {
            var question = _repository.GetObjectById(id);
            if(question != null)
            {
                return Ok(question);
            }
            return NotFound();
        }

        //PUT question/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateQuestion(int id, QuestionCreateAlterDto objectCreateAlterDto)
        {
            // filtra do banco de dados o registro que esta sendo atualizado
            Question questionfromRepo = (Question)_repository.GetObjectById(id);
            if(questionfromRepo == null)
            {
                return NotFound();
            }            
            if(questionfromRepo.AppFile!=null &&questionfromRepo.AppFile.Id.Equals(objectCreateAlterDto.AppFile.Id))
            {
                // quando o appfile esta igual em ambos objecto o entity joga uma exception dizendo 
                //que os dois estão sendo rastreados com o mesmo id
                QuestionCreateAlterDto qdto=objectCreateAlterDto;
                qdto.AppFile = questionfromRepo.AppFile;
                _mapper.Map(objectCreateAlterDto,questionfromRepo);
            }
            else{
                _mapper.Map(objectCreateAlterDto,questionfromRepo);
            }

            //_mapper.Map(objectCreateAlterDto.AppFile,questionfromRepo.AppFile);
            

            try{
                _repository.BeginTransaction();
                //salva no banco de dados o registro alterado
                _repository.SaveChanges();
                
                _repository.Commit();
            }
            catch(Exception ex)
            {
                _repository.Rollback();
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}
