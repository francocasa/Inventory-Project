var procesos = new Array;



const cargarProcesos = async () => {

    const Axios = await axios({
        method: 'GET',
        url: "/api/Processes/*?" 
    }).then(res => {
        for (i = 0; i < res.data.length; i++) {
            procesos[i] = Object.values(res.data[i]);
        }
    }).catch(err => console.log(err))
    
    cargarBotones("procesos", procesos);
};

cargarProcesos();



//--------------------------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------------------------------






//http://localhost:8090/proyecto/procesos.html


// ESTO NO COMPLE CON LAS BUENAS PRACTICAS ... GG WP
async function infUsuario() {
    ocultarElementos();
    const espera = await obtenerCaracteristicasTabla("Users")
    // caracteristicasTabla = await obtenerCaracteristicasTabla("User")
    document.getElementById("caracteristicas").style.display = 'block';
    document.getElementById("btnBuscar").style.display = 'block';
    cambiarColorBoton("infUsuario");
}

async function infProducto() {
    ocultarElementos();
    const tipoProductos = await obtenerTipoProductos()
    cargarListaDesplegable("tipoProductos", tipoProductos);
    // cuando se quiera hacer una busqueda segun producto, sirve para los procesos: 1, 2, 3, 4, 5, 6
    var combo = document.getElementById("tipoProductos");
    var selected = combo.options[combo.selectedIndex].text;

    const espera = await obtenerCaracteristicasTabla(selected)

    document.getElementById("caracteristicas").style.display = 'block';
    document.getElementById("tipoProductos").style.display = 'block';
    document.getElementById("btnBuscar").style.display = 'block';
    cambiarColorBoton("infProducto");
}

async function anaProducto() {
    ocultarElementos();
    const tipoProductos = await obtenerTipoProductos()
    cargarListaDesplegable("tipoProductos", tipoProductos);
    // cuando se quiera hacer una busqueda segun producto, sirve para los procesos: 1, 2, 3, 4, 5, 6
    var combo = document.getElementById("tipoProductos");
    var selected = combo.options[combo.selectedIndex].text;

    const espera = await obtenerCaracteristicasTabla(selected)

    document.getElementById("caracteristicas").style.display = 'block';
    document.getElementById("tipoProductos").style.display = 'block';
    document.getElementById("btnAnadir").style.display = 'block';
    cambiarColorBoton("anaProducto");
}

async function modProducto() {
    ocultarElementos();
    const tipoProductos = await obtenerTipoProductos()
    cargarListaDesplegable("tipoProductos", tipoProductos);
    // cuando se quiera hacer una busqueda segun producto, sirve para los procesos: 1, 2, 3, 4, 5, 6
    var combo = document.getElementById("tipoProductos");
    var selected = combo.options[combo.selectedIndex].text;

    const espera = await obtenerCaracteristicasTabla(selected)

    document.getElementById("caracteristicas").style.display = 'block';
    document.getElementById("tipoProductos").style.display = 'block';
    document.getElementById("btnBuscar").style.display = 'block';
    cambiarColorBoton("modProducto");
}

async function modProductosMasivo() {
    ocultarElementos();
    const tipoProductos = await obtenerTipoProductos()
    cargarListaDesplegable("tipoProductos", tipoProductos);


    document.getElementById("tipoProductos").style.display = 'block';
    document.getElementById("btnModPro").style.display = 'block';
    cambiarColorBoton("modProductosMasivo");
}

async function anaProductosMasivo() {
    ocultarElementos();
    const tipoProductos = await obtenerTipoProductos()
    cargarListaDesplegable("tipoProductos", tipoProductos);

    document.getElementById("tipoProductos").style.display = 'block';
    document.getElementById("btnAnadirMasivo").style.display = 'block';
    cambiarColorBoton("anaProductosMasivo");
}

function ocultarElementos() {

    document.getElementById("caracteristicas").style.display = 'none';
    document.getElementById("tipoProductos").style.display = 'none';
    document.getElementById("liIdentificador").style.display = 'none';

    // Ocultar Botones
    document.getElementById("btnBuscar").style.display = 'none';
    document.getElementById("btnAnadir").style.display = 'none';
    document.getElementById("btnModPro").style.display = 'none';
    document.getElementById("btnAnadirMasivo").style.display = 'none';

}

const obtenerCaracteristicasTabla = async (tabla) => {
    var caracteristicasTabla = new Array;

    const respuesta = await axios({
        method: 'GET',
        url: "/api/Dbmodels/*?"
    })
    const resultado = await respuesta.data;
    console.log(tabla)
    for (i = 0; i < resultado.length; i++) {

        if (tabla == Object.values(resultado[i])[1]) {
            caracteristicasTabla.push(Object.values(resultado[i])[2]);
            console.log(tabla)
        }

    }
    cargarEntradas("caracteristicas", caracteristicasTabla);
}

function cargarEntradas(idLista, elementos) {
    var contenido = "";

    for (i = 0; i < elementos.length; i++) {
        contenido += '<ul>' + elementos[i] + '<input type="text"></ul>';
    }
    document.getElementById(idLista).innerHTML = contenido;
}

function cargarBotones(idLista, elementos)
{
        
    var contenido = "";
    for( i = 0; i < elementos.length; i++)
    {
        contenido += '<ul><button class = "btnNoSeleccionado" id = ' + elementos[i][1] + ' onclick="' + elementos[i][1] + '()">' + elementos[i][0] + '</button></ul>';
    }
    
    document.getElementById(idLista).innerHTML = contenido;
    
}

function cargarListaDesplegable(idLista, elementos)
{
    
    var contenido = "";

    for (i = 0; i < elementos.length; i++) {
        contenido += '<ul><option value = ' + elementos[i][0] + ' >' + elementos[i][1] + '</option></ul>';
    }

    document.getElementById(idLista).innerHTML = contenido;
}

const obtenerTipoProductos = async () =>
{
    // ejecutar un GET para obtener las caracteristicas de los usuarios
    //tipoProductos = tablas; // otro se utilizara para cuando no se sepa que producto es

    var tipoProductos = new Array;

    const respuesta = await axios({
        method: 'GET',
        url: "/api/Products/*?"
    })
    const resultado = await respuesta.data;

    for (i = 0; i < resultado.length; i++) {

        tipoProductos[i] = [Object.values(resultado[i])[0], Object.values(resultado[i])[1]];
    }
    return tipoProductos;
}










function cambiarColorBoton(btnSeleccionado)
{
    for(i = 0; i < procesos.length; i++)
    {
        document.getElementById(procesos[i][1]).className = "btnNoSeleccionado";
    }
    document.getElementById(btnSeleccionado).className = "btnSeleccionado";
}

async function onChangeSelector() {
    var combo = document.getElementById("tipoProductos");
    var selected = combo.options[combo.selectedIndex].text;

    const espera = await obtenerCaracteristicasTabla(selected)
}















// ESTO NO ESTA SIENDO USADO
function nuevoElementoLista(idLista, elemento)
{
    //var lista = document.getElementById("procesos");
    var lista = document.getElementById(idLista);
    var item = document.createElement('li');
    
    item.appendChild(document.createTextNode(elemento));
    lista.appendChild(item);
};