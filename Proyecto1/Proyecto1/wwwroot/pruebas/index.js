var checkBoxSeleccionados = new Array;

const button = document.getElementById('button')

var nombreDeTablaPrincipal = "";








//ESTA SERA LA INFORMACION QUE LLEGA DE LA ANTERIOR PAGINA // DEBE DE LLEGAR TAMBEIN EL NOMBRE DE LA TABLA
async function prue()
{

    var datos = new Array;
    nombreDeTablaPrincipal = "User";
    //datos = [['userName', 'fcasanova'], ['department', 'IT'], ['annex', '26']];
    const tabla = await procesoTablas([], "Accessories");
    tablaPrincipal = document.getElementById('tablaPrincipal');
    tablaPrincipal.appendChild(tabla);

    const checkbox = await cargarCheckboxProductos(nombreDeTablaPrincipal);


}


const procesoTablas = async (restriccion, tabla) => {
    const caracteristicasTabla = await obtenerCaracteristicasTabla(tabla);
    const datosTabla = await obtenerDatosTabla(restriccion, tabla);
    datosCompletosTabla = caracteristicasTabla.concat(datosTabla);
    var tabla = generate_table(datosCompletosTabla);

    return tabla;
}



const obtenerCaracteristicasTabla = async (tabla) => {
    var caracteristicasTabla = new Array;

    const respuesta = await axios({
        method: 'GET',
        url: "/api/Dbmodels/*?"
    })
    const resultado = await respuesta.data;

    var array = new Array;

    for (i = 0; i < resultado.length; i++) {

        if (tabla == Object.values(resultado[i])[1]) {
            array.push(Object.values(resultado[i])[2]);
        }

    }
    caracteristicasTabla.push(array);

    return caracteristicasTabla;

}

//ESTO SIRVE PARA TODAS LAS TABLAS PRINCIPALES
const obtenerDatosTabla = async (datos, tabla) => {

    var stringDatos = crearListaParametros(datos);

    var datosTabla = new Array;

    const respuesta = await axios({
        method: 'GET',
        url: "/api/"+tabla+"/*?" + stringDatos
    })
    const resultado = await respuesta.data;

    for (i = 0; i < resultado.length; i++) {
        var array = new Array;
        for (j = 0; j < Object.values(resultado[i]).length; j++) {
            array.push(Object.values(resultado[i])[j]);
        }
        datosTabla.push(array);
    }

    return datosTabla;
}

function generate_table(tablaUsuario) {

    // creates a <table> element and a <tbody> element
    var tbl = document.createElement("table");
    var tblBody = document.createElement("tbody");

    // creating all cells
    for (var i = 0; i < tablaUsuario.length; i++) {
        // creates a table row
        var row = document.createElement("tr");

        for (var j = 0; j < tablaUsuario[0].length; j++) {
            // Create a <td> element and a text node, make the text
            // node the contents of the <td>, and put the <td> at
            // the end of the table row
            var cell = document.createElement("td");
            var cellText = document.createTextNode(tablaUsuario[i][j]);
            cell.appendChild(cellText);
            row.appendChild(cell);
        }

        // add the row to the end of the table body
        tblBody.appendChild(row);
    }

    // put the <tbody> in the <table>
    tbl.appendChild(tblBody);

    // sets the border attribute of tbl to 2;
    tbl.setAttribute("border", "1");

    return tbl;
}

function crearListaParametros(datos) {
    var stringDatos = "";
    if (datos[0] != null) {
        var stringDatos = datos[0][0] + "=" + datos[0][1];
        for (i = 1; i < datos.length; i++) {
            stringDatos = stringDatos + "&" + datos[i][0] + "=" + datos[i][1];
        }
    }
    return stringDatos;
}



const cargarCheckboxProductos = async (inicioId) => {
    var tipoProductos = new Array;

    const respuesta = await axios({
        method: 'GET',
        url: "/api/Products/*?"
    })
    const resultado = await respuesta.data;

    for (i = 0; i < resultado.length; i++) {

        tipoProductos[i] = [Object.values(resultado[i])[0], Object.values(resultado[i])[1], Object.values(resultado[i])[2]];
        crearCheckBox(inicioId + tipoProductos[i][1], tipoProductos[i][1], 'listaProductos');
        checkBoxSeleccionados.push(inicioId + tipoProductos[i][1]);        
    }

    return "";
}

function crearCheckBox(id, nombre, destino) {
    var label = document.createElement('label');
    label.textContent = nombre;

    var check = document.createElement('input');
    check.type = 'checkbox';
    check.id = id;

    var br = document.createElement('br');

    form = document.getElementById(destino);
    form.appendChild(check);
    form.appendChild(label);
    form.appendChild(br);
}


async function busquedaCheckBox() {
    const mostrar = await mostrarTablasSeleccionadas();


}

const mostrarTablasSeleccionadas = async () => {

    for (var i = 0; i < checkBoxSeleccionados.length; i++) {

        var check = document.getElementById(checkBoxSeleccionados[i]);
        console.log(check.checked);
        if (check.checked == true)
        {
            console.log("asdasdasd2");
            const tabla = await procesoTablas([], "accessories");
            console.log(tabla);
        }
        
    }
}



function ocultarTablas() {

    document.getElementById("tablasProductos").style.display = 'none';

}



































