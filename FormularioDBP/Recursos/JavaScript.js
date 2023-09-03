function limpiar_contenido() {
    var vacio = "";
    document.getElementById("TextboxNombre").value = vacio;
    document.getElementById("TextboxlApellido").value = vacio;
    document.getElementById("RadioButtonM").checked = vacio;
    document.getElementById("RadioButtonF").checked = vacio;
    document.getElementById("TextboxEmail").value = vacio;
    document.getElementById("TextboxDireccion").value = vacio;
    document.getElementById("city").selectedIndex = 0;
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
    ciudad = document.getElementById("city").value;
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
        direction = document.getElementById("city").value;
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

function ValidarCampos() {
    let nombre = $('#TextboxNombre').val();
    let apellido = $('#TextboxlApellido').val();
    if (nombre && apellido) {
        console.log("Se empaqueta contenido " + nombre + ", " + apellido);
        EmpaquetarContenido(nombre, apellido);
    }
    else {
        console.log("Los campos de Nombre y Apellido deben estar llenos");
    }
}

function EmpaquetarContenido(nombre, apellido) {
    console.log("EmpaquetarContenido() fue llamada.");
    $.ajax({
        url: 'Formulario.aspx/GetValidacion',
        type: 'POST',
        async: true,
        data: '{ "nombre" : "' + nombre + '", "apellidos" : "' + apellido + '"}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: exito
    });
    return false;
}

function exito(data) {
    console.log("Retornando respuesta");
    var returnS = data.d;
    if (returnS) {
        mostrar_alerta("El Nombre y Apellidos ya están registados");
        limpiar_contenido();
    }
    else {
        Swal.fire({
            title: 'Exito!',
            text: "Correcto",
            icon: 'success',
            confirmButtonText: 'Aceptar'
        });
    }
    return false;
}