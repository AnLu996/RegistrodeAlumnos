<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Segundo Laboratorio</title>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script type="text/javascript">
    function limpiar_contenido() {
        var vacio = "";
        document.getElementById("TextboxNombre").value = vacio;
        document.getElementById("TextboxlApellido").value = vacio;
        document.getElementById("RadioButtonM").checked = vacio;
        document.getElementById("RadioButtonF").checked = vacio;
        document.getElementById("TextboxEmail").value = vacio;
        document.getElementById("TextboxDireccion").value = vacio;
        document.getElementById("ciudades").selectedIndex = 0;
        document.getElementById("TextboxRequerimiento").value = vacio;

        return false;
    }

    function mostrar_alerta(text) {
        Swal.fire({
            title: 'Error!',
            text: text,
            icon: 'error',
            confirmButtonText: 'Aceptar'
        });
    }

    function validar_contenido() {
        var text, lastname, male, female, email, ciudad, direction;
        var vacio = "";
        text = document.getElementById("TextboxNombre").value;
        lastname = document.getElementById("TextboxlApellido").value;
        male = document.getElementById("RadioButtonM").checked;
        female = document.getElementById("RadioButtonF").checked;
        email = document.getElementById("TextboxEmail").value;
        direction = document.getElementById("TextboxDireccion").value;
        ciudad = document.getElementById("ciudades").value;
        if (/^[a-zA-Z]+$/.test(text) == false) {
            mostrar_alerta('Error al ingresar el nombre');
            document.getElementById("TextboxNombre").value = vacio;
            return false;
        }
        else if (/^[a-zA-Z\s\u00f1\u00d1]+$/.test(lastname) == false) {
            mostrar_alerta('Error al ingresar los apellidos');
            document.getElementById("TextboxlApellido").value = vacio;
            return false;
        }
        else if ((male == false & female == false) || (male == true & female == true)) {
            mostrar_alerta('Error al marcar el sexo');
            document.getElementById("RadioButtonM").checked = vacio;
            document.getElementById("RadioButtonF").checked = vacio;
            return false;
        }
        else if (/^[\w-]+(\.[/w-]+)*@unsa.edu.pe/.test(email) == false) {
            mostrar_alerta('Error al ingresar el email');
            document.getElementById("TextboxEmail").value = vacio;
            return false;
        }
        else if (direction == "") {
            mostrar_alerta('Error al ingresar la direccion')
            direction = document.getElementById("TextboxDireccion").value = vacio;
            return false;
        }
        else if (ciudad == "Selecciona una opcion") {
            mostrar_alerta('Error al seleccionar la ciudad');
            direction = document.getElementById("ciudades").value;
            return false;
        }
        else {
            var Dias = ['Dom', 'Lun', 'Mar', 'Mier', 'Jue', 'Vier', 'Sab'];
            var Meses = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];

            var fecha = new Date();
            var day = Dias[fecha.getDay()];
            var date = fecha.getDate();
            var month = Meses[fecha.getMonth()];
            var year = fecha.getFullYear();
            var second = fecha.getSeconds();
            var minute = fecha.getMinutes();
            var hour = fecha.getHours();
            var hora_completa = hour + ":" + minute + ":" + second;

            Swal.fire({
                title: 'Exito!',
                text: "Registrado con éxito el " + day + " " + date + "/" + month + "/" + year + " a las " + hora_completa + " GMT-0500 (hora estándar de Perú)",
                icon: 'success',
                confirmButtonText: 'Aceptar'
            });
        }
    }

</script>
<style>
    .form-check-label {
        display: inline-block;
        vertical-align: middle;
        margin-left: 10px;
    }
    body{
        background-image: url("https://fondosmil.com/fondo/4278.jpg");
    }
</style>
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous"/>
</head>
<body>
    <div class="container text-white">
    <h1 class ="text-center">Registro de Alumnos</h1>
    <form id="form1" runat="server" onsubmit="return validar_contenido();">
        <div class="mb-3 row">
            <asp:Label ID ="Name" runat="server" Text ="Nombre" class ="col-sm-1 col-form-label font-weigth-bold"></asp:Label>
            <div class="col-sm-3">
                <asp:TextBox ID="TextboxNombre" runat="server" class="form-control"></asp:TextBox><br />
            </div>
        </div>
        <div class="mb-3 row">
            <asp:Label ID ="lastname" runat="server" Text="Apellido" class ="col-sm-1 col-form-label"></asp:Label>
            <div class="col-sm-3">
                <asp:TextBox ID="TextboxlApellido" runat="server" class="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="mb-3 row">
            <asp:Label ID ="sex" runat="server" Text ="Sexo" class ="col-sm-1 col-form-label"></asp:Label>
            <div class="col">
                <div class="form-check-inline">
                    <asp:RadioButton ID ="RadioButtonM" runat="server" class="form-check-input"/>
                    <label class="form-check-label">Masculino</label>
                </div>
                <div class="form-check-inline">
                    <asp:RadioButton ID="RadioButtonF" runat="server" class="form-check-input"/>
                    <label class="form-check-label">Femenino</label>
                </div>
            </div>
        </div>
        <div class="mb-3 row">
            <asp:Label ID ="email" runat="server" Text ="Email" class ="col-sm-1 col-form-label"></asp:Label>
            <div class="col-sm-3">
                <asp:TextBox ID="TextboxEmail" runat="server" type="text" class="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="mb-3 row">
            <asp:Label ID ="direccion" runat ="server" Text ="Direccion" class ="col-sm-1 col-form-label"></asp:Label>
            <div class="col-sm-3">
                <asp:TextBox ID="TextboxDireccion" runat="server" type="text" class="form-control"></asp:TextBox>
            </div>
        </div>
       <div class="mb-3 row">
           <asp:Label ID ="ciudad" runat="server" Text ="Ciudad" class="col-sm-1 col-form-label"></asp:Label>
           <div class="col-sm-3 form-inline">
               <asp:DropDownList ID="ciudades" runat="server" class="form-select"></asp:DropDownList>     
           </div>
       </div>
       <div class="mb-3 row">
           <asp:Label ID ="requerimiento" runat="server" Text ="Requerimiento"></asp:Label><br />
           <div class="col-sm-10">
               <asp:TextBox ID="TextboxRequerimiento" runat="server" TextMode="MultiLine" rows ="5" cols="30" class="form-control"></asp:TextBox>
           </div>
       </div>
       <div class="d-flex justify-content-center">
           <asp:Button ID="Buttonl" Text="Limpiar" runat="server" OnClientClick="return limpiar_contenido();" class="btn btn-success"/>
           <asp:Button ID="ButtonE" Text="Enviar" runat="server" class="btn btn-warning" OnClick ="Button_Enviar_click"/>
       </div>
        <div class="mb-3 row">
            <div class="col-sm-10">
                <asp:TextBox ID="TextBoxRpta" runat="server" Rows="5" class="form-control" TextMode="MultiLine" Visible="false"></asp:TextBox>
            </div>
        </div>
    </form>
    </div>
</body>
</html>