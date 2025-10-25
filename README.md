# 🛍️ Propuesta Comercio - Trabajo Práctico Cuatrimestral(TPC)

## Descripción del Proyecto

Se requiere una **aplicación web** para administrar las **compras y ventas** de un negocio multipropósito.  
El sistema permitirá gestionar **clientes, proveedores, productos, marcas, categorías**, y el **registro de operaciones comerciales**.

---

## Funcionalidades Principales

### Gestión de Entidades
- **Clientes**  
  - Alta, baja y modificación.  
  - Solo los clientes registrados pueden realizar compras.

- **Proveedores**  
  - Alta, baja y modificación.  
  - Asociación con uno o más productos.

- **Productos**  
  - Administración por **marca**, **tipo** o **categoría** (todas administrables).  
  - Asociación a uno o varios proveedores.  
  - Control de **stock actual** y **stock mínimo**.

- **Marcas y Categorías**  
  - Posibilidad de alta, baja y modificación en cualquier momento.

---

## Compras

- Al registrar una **compra**, se debe indicar:
  - El **proveedor** al que se le compró.  
  - Los **productos adquiridos** y sus cantidades.  
- El sistema debe:
  - Actualizar las **líneas de stock** correspondientes.  
  - Registrar el **precio de compra más reciente** de cada producto.

---

## Ventas

- Para realizar una **venta**:
  - Se asigna un **cliente registrado**.  
  - Se seleccionan los **productos**, indicando cantidad y precios.  
- El sistema debe:
  - Validar que el **stock disponible** sea suficiente antes de confirmar la venta.  
  - Aplicar el **porcentaje de ganancia** definido para calcular el **precio de venta**:
    ```
    Precio de Venta = Precio de Compra + (Precio de Compra * Porcentaje de Ganancia)
    ```
  - Descontar automáticamente las cantidades vendidas del stock.  
  - Generar un **reporte con la factura** (incluyendo un **número de factura único**).

---

## Seguridad y Perfiles

El sistema contará con **inicio de sesión mediante usuario y contraseña**, diferenciando los siguientes perfiles:

- 👨‍💼 **Administrador**
  - Acceso total al sistema.
  - Puede gestionar todas las entidades (clientes, proveedores, productos, etc.).

- 👨‍💼 **Vendedor**
  - Puede registrar ventas.
  - No puede modificar estructuras del sistema ni administrar entidades.

---

## Tecnologías Sugeridas

- **Backend:** C# .NET Framework / .NET 8  
- **Frontend:** ASP.NET Web Forms o MVC  
- **Base de Datos:** SQL Server Express  
- **IDE:** Visual Studio 2022  

---

## 📅 Etapas de Desarrollo (TPC)

1. **Etapa 1:**  
   - Modelo de dominio (arquitectura de clases).  
   - Interfaz sin funcionalidad (pantallas base).  
   - Lectura desde base de datos de al menos una entidad.  

2. **Etapa 2:**  
   - ABMs y listados de entidades administrables.  
   - Validaciones y mejoras visuales.  

3. **Etapa 3:**  
   - Funcionalidad del core (compras, ventas, stock, reportes).  
   - Validaciones y agregados funcionales (búsquedas, recuperación de contraseña, etc.).  

4. **Etapa Final:**  
   - Seguridad y perfiles de usuario.  
   - Validaciones completas y diseño final.  

---

## 👥 Equipo de Desarrollo

**Programación 3 - UTN FRGP**  
**Equipo 1B**  
Orlando Aguilera - [@rololevy](https://github.com/rololevy)  
Albano Suárez - [@albazeraw](https://github.com/albazeraw)

---

## 📄 Licencia

Este proyecto es de uso académico en el marco de la materia Programación III de la UTN FRGP y fue desarrollado con fines educativos.
