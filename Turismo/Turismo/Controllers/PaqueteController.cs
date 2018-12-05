using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turismo.Bean;
using Turismo.Models;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mime;

namespace Turismo.Controllers
{
    public class PaqueteController : Controller
    {
        PaqueteModel model = new PaqueteModel();

        // GET: Paquete
        public ActionResult Index()
        {
            return View();
        }

        // GET: Listar
        public ActionResult List()
        {
            return View();
        }

        // GET: Listar
        public JsonResult ListarByUsuario()
        {
            Usuario x = Session["usuario"] as Usuario;
            List<Paquete> paquetes = model.ListarByUsuario(x.idusuario);
            return Json(paquetes, JsonRequestBehavior.AllowGet);
        }

        // GET: Comprar
        public ActionResult Comprar(int idpaquete)
        {
            Paquete x = model.FindById(idpaquete);
            return View(x);
        }

        [HttpPost]
        public JsonResult Comprar(Compra x)
        {
            bool result = model.Comprar(x);
            if (result)
                EnviarCorreo(x.correo,x.idpaquete,x.fechaInicio,x.total,x.npasajHabDobTriple,x.npasajHabSimple,x.celular);
            return Json(result);
        }

        public void EnviarCorreo(string correo, int idpaquete, string fechaInicio, int total, int npasajHabDobTriple, int npasajHabSimple, string celular)
        {
            Paquete x = model.FindById(idpaquete);
            Usuario user = Session["usuario"] as Usuario;
            using (SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587))
            {
                cliente.EnableSsl = true;
                cliente.Credentials = new NetworkCredential("perezgradosj@gmail.com", "mundoanimal123");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("munan.amigos@gmail.com", "Turismo Cibertec");
                message.To.Add(new MailAddress(correo));
                message.Subject = "Compra exitosa";

                message.Body =
                    "<p>Para saludarlo y darles  la noticia de que su compra fue realizada con éxito.</p>" +
                        "<html>" +
                                "<body>" +
                                    "<h1 style='color: #1a237e;'><b>Datos de paquete</b></h1>" +
                                    "<table>" +
                                        "<tbody>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Nombre</td>" +
                                                "<td style='padding: 5px; '>" + x.titulo + "</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Duración</td>" +
                                                "<td style='padding: 5px; '>" + x.dias + " dias</td>" +
                                            "</tr>" +
                                        "<tbody>" +
                                    "</table>" +
                                    "<h1 style='color: #1a237e;'><b>Datos de usuario</b></h1>" +
                                    "<table>" +
                                        "<tbody>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Correo</td>" +
                                                "<td style='padding: 5px; '>" + user.correo + "</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Celular</td>" +
                                                "<td style='padding: 5px; '>" + celular + "</td>" +
                                            "</tr>" +
                                        "<tbody>" +
                                    "</table>"+
                                    "<h1 style='color: #1a237e;'><b>Datos de compra</b></h1>" +
                                    "<table>" +
                                        "<tbody>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Total</td>" +
                                                "<td style='padding: 5px; '>S/. " + total+ "</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td style='padding: 5px; '>Fecha de inicio</td>" +
                                                "<td style='padding: 5px; '>" + fechaInicio + "</td>" +
                                            "</tr>" +
                                        "<tbody>" +
                                    "</table>";
                message.Body += "<p>Saludos cordiales</p>" +
                                "</body></html>";

                message.IsBodyHtml = true;
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                   delegate (object s
                       , X509Certificate certificate
                       , X509Chain chai
                       , SslPolicyErrors sslPolicyErrors)
                   { return true; };

                    cliente.Send(message);
                }
                catch (Exception e) {}
            }
        }

        [HttpPost]
        public JsonResult FindById(int idpaquete) {

            Paquete x = model.FindById(idpaquete);

            return Json(x);
        }

        [HttpPost]
        public JsonResult LstByRegion(string region)
        {
            List<Paquete> x = model.LstByRegion(region);
            return Json(x);
        }

        public JsonResult LstByPrecio(int precio1, int precio2)
        {
            List<Paquete> x = model.LstByPrecio(precio1, precio2);
            return Json(x,JsonRequestBehavior.AllowGet);
        }

        public JsonResult LstByCategoria(string categoria)
        {
            List<Paquete> x = model.LstByCategoria(categoria);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LstBySeguridad(string seguridad)
        {
            List<Paquete> x = model.LstBySeguridad(seguridad);
            return Json(x, JsonRequestBehavior.AllowGet);
        }
    }
}