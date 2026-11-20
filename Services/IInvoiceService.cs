using Invoice.Api.Models;

namespace Invoice.Api.Services;

public interface IInvoiceService
{
    IReadOnlyList<Invoice> GetAll();
    Invoice? GetById(int id);
    Invoice Create(Invoice item);
    bool Close(int id);
}
