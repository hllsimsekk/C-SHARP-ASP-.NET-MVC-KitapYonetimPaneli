$(document).ready(function() {
    $("[name=btnKitapListele]").bind("click", function() {
        kitapListe();
    });

    $("[name=btnKitapEkle]").bind("click", function() {
        kitapEkle();
    });
});
