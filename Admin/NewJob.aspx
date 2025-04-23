<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewJob.aspx.cs" Inherits="EmploymentAgency.Admin.NewJob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-image" style="background-image: url('../Images/bg.jpg'); background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">
            <div class="card shadow-lg">
                <div class="card-header text-center bg-primary text-white">
                    <h3><%Response.Write(Session["title"]); %></h3>
                </div> 
                <div class="card-body">
                    <div>
                        <%--<asp:Label ID="lblMsg" runat="server" CssClass="alert alert-info" Visible="false"></asp:Label>--%>
                    </div>
                    <div>
                        <div class ="btn-toolbar justify-content-between mb-3">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class ="input-group h-25">
                        <asp:HyperLink ID="linkBack" NavigateUrl="~/Admin/JobList.aspx" runat="server" CssClass ="btn btn-secondary"
                            Visible="false">< Back</asp:HyperLink>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtJobTitle" class="font-weight-bold">Job Title</label>
                            <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" placeholder="Ex. Web Developer, App Developer" required></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="txtNoOfPost" class="font-weight-bold">Number of Positions</label>
                            <asp:TextBox ID="txtNoOfPost" runat="server" CssClass="form-control" placeholder="Enter Number Of Positions" TextMode="Number" required></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-12">
                            <label for="txtDescription" class="font-weight-bold">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Job Description" TextMode="MultiLine" required></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtQualification" class="font-weight-bold">Qualification/Education Required</label>
                            <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control" placeholder="Ex. MCA, MBA..." required></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="txtExperience" class="font-weight-bold">Experience Required</label>
                            <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex: 2 Years, 1.5 Years" required></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtSpecialization" class="font-weight-bold">Specialization Required</label>
                            <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" placeholder="Enter Specialization" TextMode="MultiLine" required></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="txtLastDate" class="font-weight-bold">Last Date To Apply</label>
                            <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" placeholder="Enter Last Date To Apply" TextMode="Date" required></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtSalary" class="font-weight-bold">Salary</label>
                            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" placeholder="Ex: 25000/Month, 7L/Year" required></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="ddlJobType" class="font-weight-bold">Job Type</label>
                            <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Select Job Type</asp:ListItem>
                                <asp:ListItem>Full Time</asp:ListItem>
                                <asp:ListItem>Part Time</asp:ListItem>
                                <asp:ListItem>Remote</asp:ListItem>
                                <asp:ListItem>Freelance</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Job Type is required" ForeColor="Red" ControlToValidate="ddlJobType" InitialValue="0" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtCompany" class="font-weight-bold">Company/Organization</label>
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" placeholder="Enter Company/Organization" required></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="FuCompanyLogo" class="font-weight-bold">Company/Organization Logo</label>
                            <asp:FileUpload ID="FuCompanyLogo" runat="server" CssClass="form-control" Tooltip=".jpg, .jpeg, .png extension only" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="txtWebsite" class="font-weight-bold">Website</label>
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Enter Website" TextMode="Url"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <label for="txtEmail" class="font-weight-bold">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-12">
                            <label for="txtAddress" class="font-weight-bold">Address</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Work Address" TextMode="MultiLine" required></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-6">
                            <label for="ddlCity" class="font-weight-bold">City</label>
                            <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control" AppendDataBoundItems="true" DataTextField="CityName" DataValueField="CityName">
                                <asp:ListItem Value="0">Select City</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="City is required" ForeColor="Red" Display="Dynamic" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCity"></asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmploymentAgencyConnectionString %>" ProviderName="<%$ ConnectionStrings:EmploymentAgencyConnectionString.ProviderName %>" SelectCommand="SELECT [CityName] FROM [City]"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-lg" Text="Add Job" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
