<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Equipo1b_TPC.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-group">
        <div class="card">
            <asp:Image ID="imgVenta" runat="server" CssClass="card-img-top" ImageUrl="https://www.ceupe.com/images/easyblog_articles/3939/b2ap3_amp_venta.png" AlternateText="Ventas" />
            <div class="card-body">
                <h5 class="card-title">Ventas</h5>
                <p class="card-text">Ingreso al sistema de ventas.</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnVentas" OnClick="btnVentas_Click" CssClass="btn btn-primary" runat="server" Text="Ir a ventas" />
                </div>
            </div>
        </div>
        <div class="card">
            <asp:Image ID="imgStock" CssClass="card-img-top" ImageUrl="https://cdn-icons-png.freepik.com/512/5166/5166970.png" AlternateText="Stock" runat="server" />
            <div class="card-body">
                <h5 class="card-title">Gestion de stock</h5>
                <p class="card-text">Acesso a la gestion de stock, ver productos disponibles y modificar stock.</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnStock" OnClick="btnStock_Click" CssClass="btn btn-primary" runat="server" Text="Ir a stock" />
                </div>
            </div>
        </div>
        <div class="card">
            <asp:Image ID="imgProvedores" CssClass="card-img-top" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdsTvip-xGcAc0rXklMYadeDmDEVz49bw__Q&s" AlternateText="Provedores" runat="server" />
            <div class="card-body">
                <h5 class="card-title">Provedores</h5>
                <p class="card-text">Acesso a provedores realizar pedidos  y dar de alta nuevos provedores.</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnProvedores" CssClass="btn btn-primary" runat="server" Text="ir a provedores" OnClick="btnProvedores_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
