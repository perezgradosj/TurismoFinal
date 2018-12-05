$(document).ready(function () {
    $("#form-compra").submit(function (e) {
        e.preventDefault();

        var nsimple = parseInt($("#npasajHabSimple").val());
        var ndoble = parseInt($("#npasajHabDobTriple").val());
        var disponibles = parseInt($("#dis").val());
        var suma = nsimple + ndoble;
        if (suma > disponibles)
            alert("El numero de pasajeros no puede exceder a los pasajes disponibles");
        else {
            $.ajax({
                type: "POST",
                url: "/Paquete/Comprar",
                data: $(this).serialize(),
                success: function (result) {
                    console.log(result);
                    if (result) {
                        $("#alert-success").css("display", "block");
                        $("#alert-success").html("¡Excelente! Su compra fue realizada con éxito. Le hemos enviado un mensaje a su dirección de correo confirmandole su compra.");
                        $("#btn-terminar").attr("disabled", "disabled");
                    }
                }
            });
        }        
    });

    var suma;
    suma = parseInt($("#preciodoble").val()) + parseInt($("#preciosimple").val());
    $("#total").val(suma);

    $("#npasajHabDobTriple").change(function () {
        var ndoble = parseInt($(this).val());
        var preciodoble = parseInt($("#preciodoble").val());

        var nsimple = parseInt($("#npasajHabSimple").val());
        var preciosimple = parseInt($("#preciosimple").val());

        var total = (ndoble * preciodoble) + (nsimple * preciosimple);
        $("#total").val(total);

    });

    $("#npasajHabSimple").change(function () {
        var nsimple = parseInt($(this).val());
        var preciosimple = parseInt($("#preciosimple").val());

        var ndoble = parseInt($("#npasajHabDobTriple").val());
        var preciodoble = parseInt($("#preciodoble").val());

        var total = (ndoble * preciodoble) + (nsimple * preciosimple);
        $("#total").val(total);

    });
});