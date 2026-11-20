using Invoice.Api.Models;

namespace Invoice.Api.Services;

public class InvoiceService : IInvoiceService
{
    private readonly List<Invoice> _items = new();
    private int _nextId = 1;

    public IReadOnlyList<Invoice> GetAll() => _items;

    public Invoice? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

    public Invoice Create(Invoice item)
    {
        if (string.IsNullOrWhiteSpace(item.Title))
            throw new ArgumentException("Title is required.");
        if (item.Amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        item.Id = _nextId++;
        item.Status = "Open";
        item.CreatedAt = DateTime.UtcNow;
        _items.Add(item);
        return item;
    }

    public bool Close(int id)
    {
        var item = GetById(id);
        if (item is null || item.Status == "Closed") return false;
        item.Status = "Closed";
        return true;
    }
}
