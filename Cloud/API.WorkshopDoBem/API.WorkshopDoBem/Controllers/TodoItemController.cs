using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using API.WorkshopDoBem.DataObjects;
using API.WorkshopDoBem.Models;

// Push Notifications
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;

namespace API.WorkshopDoBem.Controllers {
    public class TodoItemController : TableController<TodoItem> {
        protected override void Initialize(HttpControllerContext controllerContext) {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<TodoItem> GetAllTodoItems() {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id) {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch) {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item) {
            TodoItem current = await InsertAsync(item);

            // Push Notification
            // Recupera as configurações do servidor (Mobile App)
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Recupera as credenciais do Notification hubs
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Cria um agente (client) do Notification Hubs
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Envia a mensagem para todos os templates registrados com a variavel "messageParam"
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = item.Text + " foi adicionado na lista.";

            try {
                // Registra o envio no log de eventos
                var result = await hub.SendTemplateNotificationAsync(templateParams);

                // Registra o sucesso de envio
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex) {
                // Registra a falha de envio
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Falhou");
            }

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id) {
            return DeleteAsync(id);
        }
    }
}