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
                                <asp:BoundField DataField="TotalOp" HeaderText="Total de operaciones" />
                                <asp:BoundField DataField="TotalE" HeaderText="Total Efectivo" />
                                <asp:BoundField DataField="TotalT" HeaderText="Total Tarjeta" />
                                <asp:BoundField DataField="TotalQR" HeaderText="Total Qr" />
                            </Columns>
                        </asp:GridView>
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
                        <div class="col-2 mt-4">
                            <asp:Button ID="btnFiltrar" CssClass="btn btn-primary" runat="server" Text="Filtrar por fecha" />
                        </div>
                        <div class="col-2 mt-4">
                            <asp:Button ID="btnLimpiarFiltros" CssClass="btn btn-danger" runat="server" Text="limpiar filtros" />
                        </div>
                    </div>
                    <div class="col-12 mt-4">
                        <asp:GridView ID="gvHistorialVentas" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay ventas almacenadas en la base de datos aun" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField DataField="TotalGeneral" HeaderText="Total venta General" />
                                <asp:BoundField DataField="TotalOp" HeaderText="Total de operaciones" />
                                <asp:BoundField DataField="fechaVenta" HeaderText="fecha de venta" />
                            </Columns>
                        </asp:GridView>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
