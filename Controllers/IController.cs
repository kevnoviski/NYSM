using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NYSM.Controllers
{
    public interface IController
    {
        ActionResult <IEnumerable<object>> GetAllObjects();
        ActionResult <object> GetObjectById(int id);
        ActionResult <object> CreateObject(object objectCreateAlterDto);
        ActionResult UpdateObject(int id, object objectCreateAlterDto);
        ActionResult DeleteObject(int id);
        ActionResult DeleteObjects(int[] id);
    }
}