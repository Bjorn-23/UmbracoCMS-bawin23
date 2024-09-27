using Umbraco.Cms.Core.Services;
using UmbracoCMS_bawin23.Models;

namespace UmbracoCMS_bawin23.Services;

public class FormServices
{

    public bool SaveFormData(CallbackRequestFormModel form, ServiceContext Services)
    {
        if (form != null && Services != null)
        {
            //logic for saving to database or other solution here
            var contentService = Services.ContentService;
            var nodeGuid = new Guid("ef54effc-afff-4973-81f8-50aaa55c8bf9");
            var nodeId = contentService!.GetById(nodeGuid);

            var callbackRequest = contentService.Create(form.Subject + " - " + form.Email + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"), nodeId, "callbackRequestItem");

            callbackRequest.SetValue("formName", form.Name);
            callbackRequest.SetValue("formEmail", form.Email);
            callbackRequest.SetValue("formPhone", form.Phone);
            callbackRequest.SetValue("formConsultingType", form.Subject);  // Make as many of these as required.

            contentService.Save(callbackRequest);
            contentService.Publish(callbackRequest, []);
            
            return true;            
        }

        return false;
    }

    public bool SaveFormData(ContactFormModel form, ServiceContext Services)
    {
        if (form != null && Services != null)
        {
            //logic for saving to database or other solution here
            var contentService = Services.ContentService;
            var nodeGuid = new Guid("aed20760-f81e-4325-bcc3-d6796dd299f2");
            var nodeId = contentService!.GetById(nodeGuid);

            var contactRequest = contentService.Create(form.Email + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"), nodeId, "contactMessageItem");

            contactRequest.SetValue("formName", form.Name);
            contactRequest.SetValue("formEmail", form.Email);
            contactRequest.SetValue("formMessage", form.Message);

            contentService.Save(contactRequest);
            contentService.Publish(contactRequest, []);

            return true;
        }

        return false;
    }

    public bool SaveFormData(OnlineSupportFormModel form, ServiceContext Services)
    {
        if (form != null && Services != null)
        {
            //logic for saving to database or other solution here
            var contentService = Services.ContentService;
            var nodeGuid = new Guid("548aed22-3430-4a5f-8172-619e43867214");
            var nodeId = contentService!.GetById(nodeGuid);

            var callbackRequest = contentService.Create(form.Email + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"), nodeId, "onlineSupportItem");
                        
            callbackRequest.SetValue("formEmail", form.Email);            

            contentService.Save(callbackRequest);
            contentService.Publish(callbackRequest, []);

            return true;
        }

        return false;
    }

}
