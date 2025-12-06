<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="VerCompra.aspx.cs" Inherits="Equipo1b_TPC.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Content/Impresion.css" media="print" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="printArea" class="container mt-4">
     <asp:UpdatePanel ID="UpVerCompras" runat="server">
         <ContentTemplate>
             <div class="col-12 mt-4 mb-4">
                 <div class="card-grid p-4  text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
                     <div style="text-align: right;">
                          <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Compra" CssClass="btn btn-secondary" OnClientClick="imprimirFactura(); return false;" />
                     </div>
                     <div class="text-center mb-3">
                         <asp:Label ID="lblDetalleFactura" CssClass="h4 text-white text-center" Text="Detalle Compra" runat="server"></asp:Label>
                     </div>
                     <div class="row mt-3 text-center">
                         <div class="col-4">
                             <asp:Label ID="lblNumeroCompra" runat="server" CssClass="text-center form-label fw-bold" Text="Numero de Compra"></asp:Label>
                             <asp:TextBox ID="txtNumeroCompra" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                         </div>
                         <div class="col-4">
                             <asp:Label ID="lblRazonSocial" runat="server" CssClass="text-center form-label fw-bold" Text="Razon Social"></asp:Label>
                             <asp:TextBox ID="txtRazonSocial" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                         </div>
                         <div class="col-4">
                             <asp:Label ID="lblFecha" runat="server" CssClass="text-center form-label fw-bold" Text="Fecha De la Compra"></asp:Label>
                             <asp:TextBox ID="txtFecha" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                         </div>
                     </div>
                     <div class="mt-4">
                         <asp:GridView ID="gvItemsCompra" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay compras que mostrar" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                             <Columns>
                                 <asp:BoundField DataField="Producto.Id" HeaderText="ID del producto"/>
                                 <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre del producto"/>
                                 <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                 <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" />
                                 <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                             </Columns>
                         </asp:GridView>
                         <div class="d-flex justify-content-center align-items-center">
                             <asp:Label ID="lblMensaje" CssClass="text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                         </div>
                     </div>

                     <div class="row mt-1">
                         <div class="col-12 d-flex justify-content-end">
                             <div style="width: auto;" class="text-center">
                                 <asp:Label ID="lblTotalCompra" runat="server" Visible="false" CssClass="text-center form-label fw-bold w-100" Text="Total"></asp:Label>
                                 <asp:TextBox ID="txtTotal" ReadOnly="true" Visible="false" CssClass="text-center form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                             </div>
                         </div>
                     </div>
             </div>
             </div>
         </ContentTemplate>
     </asp:UpdatePanel>
 </div>
 <script type="text/javascript">
     function imprimirFactura() {
         window.print();
     }
 </script>
</asp:Content>
