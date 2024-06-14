kitapListe = function (param) {
    var _data = { kitap_ad: "" };
    console.log("basladý");
    $.ajax({
        url: "/Home/kitapListe",
        type: "post",
        data: _data,
        context: document.body
    }).done(function (json) {
        var _table = $("[name=kitap_table]");

        var _tbody = $("tbody", _table);

        $("tr", _tbody).remove();

        $.each(json, function (i, e) {
          //  console.log("kitap" + i + "=>", e);

            var _tr = "<tr>";

            _tr += "<td>" + e.ad + "</td>";

            _tr += "<td>" + e.yazar_ad + "</td>";
            _tr += "<td>" + e.kategori_ad  + "</td>";
            _tr += "<td>" + e.fiyat + "</td>";

            _tr += "</tr>";

            _tbody.append(_tr);
        });
       
    });
};


kitapEkle = function () {
    var _kitapAd = $('input[name="kitapAd"]').val();
    var _fiyat = $('input[name="fiyat"]').val().replace(".", ","); // Virgülü noktaya çevir ve iki ondalýk basamakla sýnýrla                                             
    var _yazarAd = $('input[name="yazarAd"]').val(); 
    var _kategoriAd = $('input[name="kategoriAd"]').val(); 

    var _data = {
        kitapAd: _kitapAd || "",
        fiyat: _fiyat,
        yazarAd: _yazarAd || "",
        kategoriAd: _kategoriAd || "" 
    };

    $.ajax({
        type: 'POST',
        url: "/Home/kitapEkle",
        data: _data,
        context: document.body

    }).done(function (result) {
        if (result.sonuc_id === 1) {

            var yeniKitap = {
                ad: _data.kitapAd,
                fiyat: _data.fiyat,
                yazar_ad: _data.yazarAd,
                kategori_ad: _data.kategoriAd
            };


            kitapListesi.push(yeniKitap);

            $('[name=kitap_table] tbody').append('<tr><td>' + yeniKitap.ad + '</td><td>' + yeniKitap.fiyat + '</td><td>' + yeniKitap.yazar_ad + '</td><td>' + yeniKitap.kategori_ad + '</td></tr>');

            alert('Kitap eklendi. ID: ' + result.id);
            kitapEkle();
        } else {
            alert('Hata: ' + result.message);
        }
    }).fail(function (error) {
        alert('Hata: ' + error.statusText);
    });
    console.log(_data);
};

$(document).ready(function () {
    $("[name=btnKitapListele]").bind("click", function () {
        kitapListe();
        $('table[name="kitap_table"]').DataTable();      
    });

    $("[name=btnKitapEkle]").bind("click", function () {
        kitapEkle();
    });
});