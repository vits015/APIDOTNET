using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MODULOAPI.Context;
using MODULOAPI.Models;

namespace MODULOAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new{ Id = contato.Id}, contato);
        }
        [HttpGet("ObterContatos")]
        public IActionResult ObterContatos()
        {
            return Ok(_context.Contatos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato ==  null)
                return NotFound();

            return Ok(contato);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null)
                return NotFound();
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Ativo = contato.Ativo;
            contatoBanco.Telefone = contato.Telefone;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var contatoDeletado = _context.Contatos.Find(id);
            if (contatoDeletado == null)
                return NotFound();

            _context.Contatos.Remove(contatoDeletado);
            _context.SaveChanges();

            return Ok($"O contato de {contatoDeletado.Nome} foi removido com sucesso!");            
        }
        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x=> x.Nome.Contains(nome));
            return Ok(contatos);
        }

    }
}