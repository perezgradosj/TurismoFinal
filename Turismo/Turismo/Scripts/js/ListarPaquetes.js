$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Paquete/ListarByUsuario",
        success: function (result) {
            console.log(result);
            $.each(result, function (key, rs) {
                $("#table tbody").append(
                    "<tr>" +
                    "<td>" + (key + 1) + "</td>" +
                    "<td>" + rs.titulo + "</td>" +
                    "<td>" + rs.destino + "</td>" +
                    "<td>" + rs.fecha_inicio+ "</td>" +
                    "<td><img src='../Img/" + rs.foto1Name + "' class='rounded mx-auto d-block' style='width:60px;height:60px;' /> </td>" +
                    "<td><button class='btn btn-info btn-sm btnDetalle' value='" + rs.id + "' data-toggle='modal' data-target='#detalleModal'>Ver detalle</button></td>"+
                    "</tr>"
                );
            });
        }
    });

    //Detalle de paquete
    $("#table tbody").on("click", ".btnDetalle", function () {
        var id = $(this).val();
        $("#iditinerarios tbody").empty();
        $("#idalimentacion tbody").empty();
        $.ajax({
            type: "POST",
            url: "/Paquete/FindById",
            data: { idpaquete: id },
            dataType: "json",
            success: function (result) {
                console.log(result);
                console.log(result.itinerarios);

                $.each(result.itinerarios, function (key, rs) {
                    $("#iditinerarios tbody").append(
                        "<tr style='text-align: center'>" +
                        "<td>" + rs.dia + "</td>" +
                        "<td>" + rs.descripcion + "</td>" +
                        "</tr>"
                    );
                });
                $.each(result.itinerarios, function (key, rs) {
                    $("#idalimentacion tbody").append(
                        "<tr style='text-align: center'>" +
                        "<td>" + rs.dia + "</td>" +
                        "<td>" + rs.alojamiento + "</td>" +
                        "<td>" + rs.alimentacion + "</td>" +
                        "</tr>"
                    );
                });
            }
        });
    });
});