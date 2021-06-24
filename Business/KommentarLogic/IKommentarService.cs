using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;

namespace ch.gibz.m151.projekt.Business.KommentarLogic
{
    public interface IKommentarService
    {
        public List<Comment> GetForArticle(string id);

        public void DeleteComment(int id);

        public CreatedKommentar CreateOrUpdate(Comment incomingComment);

        public Beitrag GetBeitrag(int id);

        public void LikeComment(int id);

        public void DislikeComment(int id);
    }
}
