<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EmploymentAgency.User.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container pt-2 pb-5">
        <div class="main-body">
            <asp:DataList ID="dlProfile" runat="server" Width="100%" OnItemCommand="dlProfile_ItemCommand">
                <ItemTemplate>
                    <div class="row gutters-sm">
                        <!-- Profile Picture Card -->
                        <div class="col-md-4 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="d-flex flex-column align-items-center text-center">
                                        <!-- Image dynamically bound to Base64 string -->
                                        <img id="userImage" src="data:image/png;base64,<%# Eval("Image") %>" alt="UserPic" class="rounded-circle" width="150" />

                                        <div class="mt-3">
                                            <h4 class="text-capitalize"><%# Eval("Name") %></h4>
                                            <p class="text-secondary mb-1"><%# Eval("Username") %></p>
                                            <p class="text-muted font-size-sm text-capitalize">
                                                <i class="fas fa-map-marker-alt"></i><%# Eval("City") %>
                                            </p>
                                        </div>

                                        <!-- File input to change image -->
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                                        <asp:Button ID="btnUpdateImage" runat="server" Text="Update Image" OnClick="btnUpdateImage_Click" CssClass="btn btn-primary mt-3" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Profile Details Card -->
                        <div class="col-md-8">
                            <div class="card mb-3">
                                <div class="card-body">
                                    <h4 class="mb-3">Profile Details</h4>

                                    <div class="row mb-2">
                                        <div class="col-sm-3">
                                            <h6 class="mb-0">Full Name</h6>
                                        </div>
                                        <div class="col-sm-9 text-secondary"><%# Eval("Name") %></div>
                                    </div>
                                    <hr />

                                    <div class="row mb-2">
                                        <div class="col-sm-3">
                                            <h6 class="mb-0">Email</h6>
                                        </div>
                                        <div class="col-sm-9 text-secondary"><%# Eval("Email") %></div>
                                    </div>
                                    <hr />

                                    <div class="row mb-2">
                                        <div class="col-sm-3">
                                            <h6 class="mb-0">Phone</h6>
                                        </div>
                                        <div class="col-sm-9 text-secondary"><%# Eval("PhoneNumber") %></div>
                                    </div>
                                    <hr />

                                    <div class="row mb-2">
                                        <div class="col-sm-3">
                                            <h6 class="mb-0">Address</h6>
                                        </div>
                                        <div class="col-sm-9 text-secondary"><%# Eval("Address") %></div>
                                    </div>
                                    <hr />

                                    <div class="row mb-2">
                                        <div class="col-sm-3">
                                            <h6 class="mb-0">Resume Upload</h6>
                                        </div>
                                        <div class="col-sm-9 text-secondary">
                                            <%# Eval("Resume") == DBNull.Value ? "Not Uploaded" : "Uploaded" %>
                                        </div>
                                    </div>
                                    <hr />

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit"
                                                CssClass="btn btn-outline-primary"
                                                CommandName="EditUserProfile"
                                                CommandArgument='<%# Eval("UserID") %>' />
                                        </div>
                                    </div>

                                </div> <!-- End card-body -->
                            </div> <!-- End card -->
                        </div> <!-- End right column -->
                    </div> <!-- End row -->
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
    