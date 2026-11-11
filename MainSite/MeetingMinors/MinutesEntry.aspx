<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MinutesEntry.aspx.cs" Inherits="MeetingMinors_MinutesEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
<style>
    fieldset.for-panel {
        background-color: #fcfcfc;
        border: 1px solid #999;
        padding: 15px 10px;
        background-color: white;
        margin-bottom: 12px;
    }

    fieldset.for-panel legend {
        background-color: #fafafa;
        border: 1px solid #ddd;
        border-radius: 1px;
        font-size: 12px;
        font-weight: bold;
        line-height: 10px;
        margin: inherit;
        padding: 7px;
        width: auto;
        margin-bottom: 0;
        color: black;
    }
</style>    

      <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Minutes Entry  </h1>
                        </div>
                        <%--                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                   
                        
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" /></div>
                            </div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">From Date:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date" ID="DropDownList1"  class="form-control form-control-sm" /></div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">To Date:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="DropDownList2"  class="form-control form-control-sm" /></div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Information:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="DropDownList3"  class="form-control form-control-sm" /></div>
                            </div>


                            </div>
                        </div>
             </div>
       
       
        <div class="col-md-4 tempclass">
                                           
                                                 
                                                        <div class="divider"></div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group row">
                                                                    <div class="col-sm-5 col-md-5 label-align">
                                                                        <span class="lbl">Title:</span>
                                                                    </div>
                                                                    <div class="col-sm-7 col-md-7">
                                                                        <asp:Label class="sub-lbl" ID="lblEmployeeId" runat="server" Text=""></asp:Label>
                                                                       
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group row">
                                                                    <div class="col-sm-5 col-md-5 label-align">
                                                                        <span class="lbl"> Purpose:</span>
                                                                    </div>
                                                                    <div class="col-sm-7 col-md-7">
                                                                        <asp:Label class="sub-lbl" ID="lblEmployeeName" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group row">
                                                                    <div class="col-sm-5 col-md-5 label-align">
                                                                        <span class="lbl">Category:</span>
                                                                    </div>
                                                                    <div class="col-sm-7 col-md-7">
                                                                        <asp:Label class="sub-lbl" ID="lblDateOfJoining" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group row">
                                                                    <div class="col-sm-5 col-md-5 label-align">
                                                                        <span class="lbl">Date:</span>
                                                                    </div>
                                                                    <div class="col-sm-7 col-md-7">
                                                                        <asp:Label class="sub-lbl" ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
            
            <div class="divider"></div>
                                                    </div>
                                             
       
       

       
           <div class="form-row">
                            <div class="col-1">
                                <div class="form-group">
                                    <label> Participants </label>  <span class="star-mark"></span>
                                </div>
                               
                            </div>
               
               
                            <div class="col-2">
                                <div class="form-group">
                                             
                                

                                </div>
                                <div class="form-group">
                                             
                                    <asp:CheckBox ID="CheckBox4" Text="Emp01" runat="server"/>

                                </div>
                                <div class="form-group">

                                    <asp:CheckBox ID="CheckBox8" Text="Emp02 " runat="server"/>

                                </div>
                                
                                <div class="form-group">

                                    <asp:CheckBox ID="CheckBox6" Text="Emp03" runat="server"/>

                                </div>


                            </div>
                            
                         

                        </div>
       
   <div class="form-row">
 
       <div class="col-md-10">
           


           <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
           <div class="ui-group-buttons">

               <asp:Button runat="server" ID="btn_Save" OnClientClick="return confirm('Are you sure you want to Save ?')"  Text="  Download Excel  " CssClass="btn btn-sm btn-info" />
               <div class="or or-sm"></div>
               <asp:Button runat="server" ID="btnNext" OnClientClick="return confirm('Are you sure you want to Save & Next ?')" Text="    UpLoad Excel   " CssClass="btn btn-sm btn-success" />

           </div>
       </div>
    
       
     
   </div>


       <br/>
       
       <table class="table table-bordered dataTable dtr-inline">
           <tr>
    <th>SL</th>
    <th>Topics & Issue</th>
    <th>Discussion </th> 
    <th>Responsible</th>
               
               <th>Deadline</th>
               <th>Remarks</th>

  </tr>
  
  
  <tr>
    <td> 01</td>
    <td> Topics 01</td>
    <td>Discussion</td> 
    <td>Deadline</td>
<td>Deadline </td>
      <td>
          Remarks
      </td>

  </tr>
           
  <tr>
      <td> 02</td>
      <td> Topics 02</td>
      <td>Discussion</td> 
      <td>Deadline</td>
      <td>Deadline </td>
      <td>
          Remarks
      </td>
  </tr>
           
             
  <tr>
      <td> 03</td>
      <td> Topics 03</td>
      <td>Discussion</td> 
      <td>Deadline</td>
      <td>Deadline </td>
      <td>
          Remarks
      </td>

  </tr>
       </table>
       
       <br/>

   
      <fieldset class="for-panel">
                                <legend>Document Upload </legend>
                                        <div class="form-row">
                                            <div class="col-4">
                                            
                                            </div>


                                          

                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Document Upload</label>
                                                    <div>

                                                        <input type="file" name="NomineeImageUpload" accept="image/*" />
                                                        <input type="button" id="btnNomineeImageUpload" value="Document Upload" />
                                                        <progress id="NomineeImageProgress" style="display: none"></progress>
                                                        <hr />
                                                        <asp:Image ID="img_NomineeImage" runat="server" Width="120" Height="90" BorderWidth="1" />
                                                        <asp:HiddenField runat="server" ID="hfNomineeImage" />
                                                        <span id="lblMessageNomineeImage" style="color: Green"></span>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            
                                            <div class="col-4">
                                            
                                            </div>

                                        </div>
                                               
                                               </fieldset>
   <br/>
   
   <div class="form-row">

       <div class="col-md-4">
       </div>
       <div class="col-md-4">
           <div class="form-row">
               <div class="col-md-6">
                   <asp:Button ID="Button3" Text="Save " CssClass="btn btn-block btn-info" runat="server" />
               </div>
               <div class="col-md-6">
                   <asp:Button ID="Button4" Text="Submit" CssClass="btn btn-block warning" runat="server"  BackColor="#FFCC00" />
               </div>
           </div>
           <div class="form-group">
           </div>
       </div>

       <div class="col-md-4">
       </div>
   </div>

      
              </div>
        
        </div>
                               </div>
        
        </div>
      

</asp:Content>

