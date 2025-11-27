<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="resumenVenta.aspx.cs" Inherits="Equipo1b_TPC.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card-grid p-4  text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:UpdatePanel ID="UpResumenVenta" runat="server">
                <ContentTemplate>
                    <div class="col-12 mt-4">
                        <h4 class="text-white text-center">Resumen general de ventas - <%=DateTime.Now.ToString("dd/MM/yyyy")%></h4>
                        <asp:GridView ID="gvResumenVenta" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay resumen de venta para mostrar" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField DataField="TotalGeneral" HeaderText="Total venta General" />
                                <asp:BoundField DataField="TotalFa" HeaderText="Total Factura(A)" />
                                <asp:BoundField DataField="TotalFb" HeaderText="Total Factura(B)" />
                                <asp:BoundField DataField="TotalFc" HeaderText="Total Factura(C)" />
                                <asp:BoundField DataField="TotalOperaciones" HeaderText="Total de operaciones" />
                                <asp:BoundField DataField="TotalEfectivo" HeaderText="Total Efectivo" />
                                <asp:BoundField DataField="TotalTarjeta" HeaderText="Total Tarjeta" />
                                <asp:BoundField DataField="TotalQR" HeaderText="Total Qr" />
                            </Columns>
                        </asp:GridView>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:Button ID="btnCierreCaja" runat="server" OnClick="btnCierreCaja_Click" CssClass="btn btn-danger" Text="Generar ciere de caja" />
                        </div>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:Label ID="lblMensaje" CssClass="text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
    </div>
    <div class="container mt-4">
        <div class="card-grid p-4 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:UpdatePanel ID="upHistorialVenta" runat="server">
                <ContentTemplate>
                    <h4 class="text-white text-center">Historial de ventas - Selecione una fecha por la cual filtrar</h4>
                    <div class="row justify-content-center align-items-center">
                        <div class="col-4 text-center">
                            <asp:Label ID="lblDesde" runat="server" CssClass="form-label" Text="Desde"></asp:Label>
                            <asp:TextBox ID="txtFechaDesde" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-4 text-center">
                            <asp:Label ID="lblHasta" runat="server" Text="Hasta" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtFechaHasta" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-4 mt-4">
                            <asp:Button ID="btnFiltrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" runat="server" Text="Filtrar por fecha" />
                            <asp:Button ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-danger" runat="server" Text="limpiar filtros" />
                        </div>
                    </div>
                    <div style="max-height:400px; overflow-y:auto; overflow-x:hidden; background:white; margin-top:12px;">
                            <asp:GridView ID="gvHistorialVentas" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay ventas historicas almacenadas" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="TotalGeneral" HeaderText="Total venta General" />
                                    <asp:BoundField DataField="TotalOperaciones" HeaderText="Total de operaciones" />
                                    <asp:BoundField DataField="fechaResumenVenta" HeaderText="fecha de venta" />
                                </Columns>
                            </asp:GridView>
                    </div>
                    <div class="d-flex justify-content-center align-items-center">
                        <asp:Label ID="lblMensajeHistorial" CssClass="text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
