

using Microsoft.AspNetCore.Mvc;

namespace KisiselAdresDefteri.Web.Models;

public class KisiRepository
{
    private static List<Kisiler> _kisilers = new List<Kisiler>()
    {
    };



    //yukarıdakı listemizi aşığıda tanımladığımız metotlarla berabaer dış dünyaya açacaz
    //yani başka classların bu repository üstünde işlem yapmamızı sağlayacağız

    // Tüm Kişileri Dönecek metodumuz
    public List<Kisiler> GetAll() => _kisilers;

    //kişi ekleme

    public void Add(Kisiler newKisiler) => _kisilers.Add(newKisiler);

    //kişi silme
    public void Remove(int id)
    {
        //bu person varmı yokmu onun kontrolu
        var hasPerson = _kisilers.FirstOrDefault(x => x.Id == id);
        if (hasPerson == null)
        {
            throw new Exception($"Bu id{id} ye sahip ürün bulunmamaktadır");
        }

        _kisilers.Remove(hasPerson);
    }

    //Ürün Güncelleme
    public void Update(Kisiler updateKisiler)
    {
        //önce güncellenecek nesne varmı bunu tespit edelim
        var hasPerson = _kisilers.FirstOrDefault(x => x.Id == updateKisiler.Id);

        if (hasPerson == null)
        {
            throw new Exception($"Bu id{updateKisiler.Id} ye sahip ürün bulunmamaktadır");
        }

        hasPerson.Name = updateKisiler.Name;
        hasPerson.Surname = updateKisiler.Surname;
        hasPerson.PhoneNumber = updateKisiler.PhoneNumber;
        hasPerson.Email = updateKisiler.Email;
        hasPerson.Address = updateKisiler.Address;

        //dizindeki indexi bulma
        var index = _kisilers.FindIndex(x => x.Id == updateKisiler.Id);

        _kisilers[index] = hasPerson;
    }
};


