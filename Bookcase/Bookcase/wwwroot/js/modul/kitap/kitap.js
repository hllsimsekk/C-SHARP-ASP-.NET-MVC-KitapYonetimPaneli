$(document).ready(function () {
    var _grid = $("#dtKitapListesi");

    _grid.unbind().undelegate();

    _grid.on("click", ".button-edit", function () {
        var _tr = $(this).closest("tr"); // closest ile týklanan butonun bulunduðu satýrý alýyoruz

        if (_tr[0] != null) {
            var _data = _grid.DataTable().row(_tr).data();
            kitapOku(_data.id);
            
        }
    });

    kitapListele();

    $("[name=btnListele]").bind("click", kitapListesiYenile);

    $("[name=btnEkle]").bind("click", kitapEkle);  

    $("[name=btnSil]").bind("click", function () {
        var id = $("[name=id]").val();
        kitapSil(id);
    });
});


kitapOku = function (id) {
    var _form = $("#kitapDetayModel");

    $("[name=id]", _form).val(id);
    _form.modal("show");

    $.ajax({
        type: 'POST',
        url: "/Home/KitapOku",
        data: { id: id },
        context: document.body
    }).done(function (result) {
        $("[name=ad]", _form).val(result.ad);
        kitapKategoriListesi(result.kategori_id);
        kitapYazarListesi(result.yazar_id);
        $("[name=fiyat]", _form).val(result.fiyat);
    }).fail(function (error) {
        alert('Hata: ' + error.statusText);
    });
};

kitapListele = function () {
    var _table = new DataTable('#dtKitapListesi', {
        fixedHeader: true,
        bPaginate: true,
        bFilter: false,
        bSort: false,
        ajax: {
            url: 'Home/kitapListe',
            type: 'POST',
            data: function (d) {
                d = kitapListeParametreOku(d);
            }
        },
        columns: [
            { data: 'detay', order: { type: 'button', buttons: 'd' }, sClass: 'button-edit grid-button' },
            { data: 'ad' },
            { data: 'yazar_ad' },
            { data: 'kategori_ad' },
            { data: 'fiyat' }
        ],
        processing: true,
        serverSide: true
    });
};

kitapListeParametreOku = function (data) {
    return data;
};

kitapListesiYenile = function () {
    $("#dtKitapListesi").DataTable().ajax.reload();
};

kitapKategoriListesi = function (kategori_id) {
    var _form = $("#kitapDetayModel");
    var _select = $("[name=kategori_id]", _form);

    if ($("option", _select).length == 0) {
        $.ajax({
            type: 'POST',
            url: "/Home/KitapKategoriListe",
            context: document.body
        }).done(function (result) {
            $.each(result, function (i, e) {
                var _op = "<option value=" + e.id + ">" + e.kategori_ad + "</option>";
                _select.append(_op);
            });
            _select.val(kategori_id);
        }).fail(function (error) {
            alert('Hata: ' + error.statusText);
        });
    } else {
        _select.val(kategori_id);
    }
};

kitapYazarListesi = function (yazar_id) {
    var _form = $("#kitapDetayModel");
    var _select = $("[name=yazar_id]", _form);

    if ($("option", _select).length == 0) {
        $.ajax({
            type: 'POST',
            url: "/Home/KitapYazarListe",
            context: document.body
        }).done(function (result) {
            $.each(result, function (i, e) {
                var _op = "<option value=" + e.id + ">" + e.yazar_ad + "</option>";
                _select.append(_op);
            });
            _select.val(yazar_id);
        }).fail(function (error) {
            alert('Hata: ' + error.statusText);
        });
    } else {
        _select.val(yazar_id);
    }
};



kitapEkle = function () {
    var _form = new bootstrap.Modal($('#kitapEkleModel')[0]);
    _form.show();

    $('#kitapEkleModel .btn-primary').unbind('click').bind('click', function () {
        var postData = {
            kitapAd: $('[name=kitap_ad]').val(),
            fiyat: Number($('[name=fiyat2]').val().replace(',', '.')),
            yazarAd: $('[name=yazar_ad]').val(),
            kategoriAd: $('[name=kategori_ad]').val()
        };

        console.log("kitap.js veriler: " + postData.fiyat);
        console.log("tipi: " + typeof postData.fiyat);

        $.ajax({
            type: 'POST',
            url: '/Home/KitapEkle',
            data: postData,
            success: function (result) {
                var successAlert = '<div class="alert alert-success alert-custom" role="alert">Ekleme islemi basarili</div>';
                $("body").append(successAlert);
                setTimeout(function () {
                    $(".alert-success").remove();
                }, 5000);
                if (result.sonuc_id == 1) {
                    kitapListesiYenile();
                    _form.hide();
                }
            },
            error: function (error) {
                alert('Hata: ' + error.statusText);
            }
        });
    });
};



function kitapSil(id) {
    var _sform = $("#kitapDetayModel");
    console.log("data_id =>" + id);
    if (confirm("Emin misiniz?")) {
        $.ajax({
            type: 'POST',
            url: '/Home/KitapSil',
            data: { kitapId: id },
            success: function (result) {
                var successAlert = '<div class="alert alert-success alert-custom" role="alert">Ýslem basarili.</div>';
                $("body").append(successAlert);
                setTimeout(function () {
                    $(".alert-success").remove();
                }, 5000);
                if (result.sonuc_id == 1) {
                    kitapListesiYenile();
                    _sform.modal("hide");
                }
            },
            error: function (error) {
                alert('Hata: ' + error.statusText);
            }
        });
    }
};


$(document).ready(function () {
    $("[name=btnEkle]").on("click", kitapEkle);
    $("[name=btnSil]").on("click", kitapSil);
});


