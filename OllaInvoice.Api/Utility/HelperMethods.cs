using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace OllaInvoice.Api.Utility
{
    public static class HelperMethods
    {

        
        public static IFormFile ReturnFormFile(FileStreamResult result)
        {
            var ms = new MemoryStream();
            try
            {
                result.FileStream.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "name", "Invoice");
            }
            catch (Exception)
            {
                //ms.Dispose();
                throw;
            }
            finally
            {
                //ms.Dispose();
            }
        }

        public static IFormFile ReturnFormFile(byte[] result)
        {
            try
            {
                var stream = new MemoryStream(result);
                
                return new FormFile(stream, 0, stream.Length, "name", "Invoice");
            }
            catch (Exception)
            {
                //ms.Dispose();
                throw;
            }
            finally
            {
                //ms.Dispose();
            }
        }

        public static string GetTemplateString()
        {
            return @"<!DOCTYPE html>
<html lang = 'en' xmlns = 'http://www.w3.org/1999/xhtml' xmlns: v = 'urn:schemas-microsoft-com:vml' xmlns: o = 'urn:schemas-microsoft-com:office:office' >
         <head >
         
             <meta charset = 'utf-8'>
               
                   <meta name = 'viewport' content = 'width=device-width' > 
                   <meta http-equiv = 'X-UA-Compatible' content = 'IE=edge' >
               
                   <meta name = 'x-apple-disable-message-reformatting' >  
               
                   <title ></title > 
               


                   <link href = 'https://fonts.googleapis.com/css?family=Playfair+Display:400,400i,700,700i' rel = 'stylesheet' >
               

                   
               
                   <style >
               @font-face {
                    font-family: 'Graphik';
                src: url('images/Graphik-Font-Family/GraphikBlack.otf') format('opentype');
                }
@font-face {
                    font-family: 'Graphik-thin';
                src: url('images/Graphik-Font-Family/GraphikThin.otf') format('opentype');
                }
@font-face {
                    font-family: 'Graphik-regular';
                src: url('images/Graphik-Font-Family/GraphikRegular.otf') format('opentype');
                }

                html,
body {
                margin: 0 auto!important;
                padding: 0!important;
                height: 100% !important;
                width: 100% !important;
                background: #092F44;


                    -ms-text-size-adjust: 100%;
                    -webkit-text-size-adjust: 100%;
                }

             
                div[style *= 'margin: 16px 0'] {
                margin: 0!important;
                }

                
                table,
td {
                    mso-table-lspace: 0pt!important;
                    mso-table-rspace: 0pt!important;
                }

               
                table {
                    border-spacing: 0!important;
                    border-collapse: collapse!important;
                    table-layout: fixed !important;
                    margin: 0 auto!important;
                }

              
                img {
                    -ms-interpolation-mode:bicubic;
                }

                a {
                    text-decoration: none;
                }


                *[x-apple-data-detectors],  /* iOS */
.unstyle-auto-detected-links *,
.aBn {
                    border-bottom: 0!important;
                cursor: default!important;
                color: inherit!important;
                    text-decoration: none!important;
                    font-size: inherit!important;
                    font-family: inherit!important;
                    font-weight: inherit!important;
                    line-height: inherit!important;
                }


.a6S {
                display: none!important;
                opacity: 0.01!important;
                }


.im {
                color: inherit!important;
                }

          
                img.g-img + div {
                display: none!important;
                }


@media only screen and(min-device-width: 320px) and(max-device-width: 374px) {
                    u ~div.email-container {
                        min-width: 320px!important;
                    }
    .main_msg {
                        font-size: 12px;
                    }
                }

@media only screen and(min-device-width: 375px) and(max-device-width: 413px) {
                    u ~div.email-container {
                        min-width: 375px!important;
                    }
    .text.main_msg {
                        font-size: 12px;
                    }
                }

@media only screen and(min-device-width: 414px) {
                    u ~div.email-container {
                        min-width: 414px!important;
                    }
    .text.main_msg {
                        font-size: 12px;
                    }
                }

    </style>

  
  <style>


.primary{
                background: #f3a333;
}

.bg_white{
                background: #ffffff;
}
.bg_light{
                background: #fafafa;
}
.bg_black{
                background: #000000;
}
.bg_dark{
                background: rgba(0, 0, 0, .8);
                }
.bg_blue {
                background: #2C4A97;
}
.email-section{
                padding: 10em 15px 2em;
                }


.btn{
                padding: 15px 84px;
                }
.btn.btn-primary{
                    border-radius: 2px;
                background: #2C4A97;
	color: #FFFFFF;
}



                h1,h2,h3,h4,h5,h6{
                    font-family: 'Playfair Display', serif;
                color: #000000;
	margin-top: 0;
                }

                body{
                    font-family: 'Graphik-regular', sans-serif;
                    font-weight: 400;
                    font-size: 15px;
                    line-height: 1.8;
                color: rgba(0, 0, 0, .4);
                }

                a{
                color: #f3a333;
}



.logo h1{
                margin: 0;
                }
.logo h1 a{
                color: #000;
	font-size: 20px;
                    font-weight: 700;
                    text-transform: uppercase;
                    font-family: 'Montserrat', sans-serif;
                }


.hero{
                position: relative;
                height: 424px;
                }

.text {
                    font-size: 18px;
                    font-weight: 100;
                }
.hero.text{
                color: rgba(255, 255, 255, .8);
                }
.hero.text h2{
                color: #ffffff;
	font-size: 30px;
                    margin-bottom: 0;
                }



.heading-section h2{
                color: #000000;
	font-size: 28px;
                    margin-top: 0;
                    line-height: 1.4;
                }
.heading-section.subheading{
                    margin-bottom: 20px!important;
                display: inline-block;
                    font-size: 13px;
                    text-transform: uppercase;
                    letter-spacing: 2px;
                color: rgba(0, 0, 0, .4);
                position: relative;
                }
.heading-section.subheading::after{
                position: absolute;
                left: 0;
                right: 0;
                bottom: -10px;
                content: '';
                width: 100%;
                height: 2px;
                background: #f3a333;
	margin: 0 auto;
                }

.heading-section-white{
                color: rgba(255, 255, 255, .8);
                }
.heading-section-white h2{
                    font-size: 28px;
                    font-family: 'Graphik';
                    line-height: 1;
                    padding-bottom: 0;
                }
.heading-section-white h2{
                color: #ffffff;
}
.heading-section-white.subheading{
                    margin-bottom: 0;
                display: inline-block;
                    font-size: 13px;
                    text-transform: uppercase;
                    letter-spacing: 2px;
                color: rgba(255, 255, 255, .4);
                }


.icon{
                    text-align: center;
                }



.text-services{
                padding: 10px 10px 0;
                tex-align: center;
                }
.text-services h3{
                    font-size: 20px;
                }


.text-services.meta{
                    text-transform: uppercase;
                    font-size: 14px;
                }

.text-testimony.name{
                margin: 0;
                }
.text-testimony.position{
                color: rgba(0, 0, 0, .3);

                }



.img{
                width: 100%;
                height: auto;
                position: relative;
                }
.img.icon{
                position: absolute;
                top: 50%;
                left: 0;
                right: 0;
                bottom: 0;
                    margin-top: -25px;
                }
.img.icon a{
                display: block;
                width: 60px;
                position: absolute;
                top: 0;
                left: 50%;
                    margin-left: -25px;
                }

.main_msg {
                padding: 0 15px;
                    padding-bottom: 145px;
                }

@media screen and(max-width: 475px) {
  .main_msg > p {
                        font-size: 14px;
                    }
                }



.counter-text{
                    text-align: center;
                }
.counter- text.num{
                display: block;
                color: #ffffff;
	font-size: 34px;
                    font-weight: 700;
                }
.counter-text.name{
                display: block;
                color:rgba(255, 255, 255, .9);
                    font-size: 13px;
                }



.footer{
                color: rgba(255, 255, 255, .5);

                }
.footer.heading{
                color: #ffffff;
	font-size: 20px;
                }
.footer ul{
                margin: 0;
                padding: 0;
                }
.footer ul li{
                    list-style: none;
                    margin-bottom: 10px;
            }
.footer ul li a{
                color: rgba(255, 255, 255, 1);
                }


@media screen and(max-width: 500px) {

  .hero {
                        line-height: 1.5;
                    height: 350px;
                    }
	.icon{
                        text-align: left;
                    }

	.text-services{
                        padding-left: 0;
                        padding-right: 20px;
                        text-align: left;
                    }

                
                }
    </style>


</head >

<body width = '100%' style = 'margin: 0; padding: 0 !important; mso-line-height-rule: exactly; background-color: #FFFFFF;' >
   
       <center style = 'width: 100%; background-color: #f1f1f1;' >
    
            <div style = 'display: none; font-size: 1px;max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden; mso-hide: all; font-family: sans-serif;' >
                 &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp; &zwnj; &nbsp;
        </div >
        <div style = 'max-width: 600px; margin: 0 auto;' class='email-container'>
          
            <table align = 'center' role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='margin: auto;'>
                <tr>
                    <td valign = 'middle' class='hero bg_light' style='background-image: url(./images/rectangle.jpg); background-size: contain; background-repeat: no-repeat;'>
                        <table>
                            <tr>
                                <td>
                                    <div style = 'text-align: center; margin-bottom: 10px;' >
                                        <h1 style='font-family: Graphik-thin; color: #FFFFFF; margin-top: 2rem'>Welcome!</h>
                                    </div>
                                    <div class='text main_msg' style='text-align: left; color: #2C4A97;'>
                                        <p style = 'margin: 0; margin-bottom: 10px; font-family:Graphik; color:#2C4A97;' > Hi, #name,</p>
                                        <p style = 'margin: 0; font-family: Graphik; color:#2C4A97; text-align: center;' >
                                            How do you plan to kick off this new month? What will you like to achieve?
                                            I have an idea! A great way you can start this month is to apply for our March 2022 cohort as applications are still open!
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style = 'position: relative;' >
                    <td class='bg_light' style='text-align:center; color: #000000; position: absolute; width: 100%; top: -55px;'>
                        <table>
                            <tr>
                                <td style = 'padding: 0 15px;' >
                                    <p>
                                        <a href='#url' class='btn btn-primary'>Open</a>
                                    </p>
                                    <p style = 'margin: 0; padding-top: 30px; font-size: 18px;' >
                                        or copy and paste this link into your browser:
                                    </p>
                                    <p style = 'margin: 0; font-size: 18px; color: #2C4A97; text-decoration: underline; font-family: Graphik;'>
                                        https://ollasystems.com
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </t>
                </tr>
                <tr>
                    <td valign = 'middle' class='bg_light footer email-section'>
                        <table>
                            <tr>
                                <td valign = top width='33.333%'>
                                    <table role = 'presentation' cellspacing=0; cellpadding=0 border=0; width=100%;>
                                        <tr>
                                            <td style = 'text-align: left; padding-right: 10px; color: #000000;' >
                                                <p style='margin: 0;'>Love,</p>
                                                <p style = 'margin: 0; font-family: Graphik;' > The Olla Systems Team</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign = 'top' width='33.333%'>
                                    <table role = presentation cellspacing=0 cellpadding=0 border=0 width='100%'>
                                        <tr>
                                            <td style = 'text-align: right; padding-left: 5px; padding-right: 5px;' >
                                                <img src='https://ollapos.ollasystems.com/assets/images/Logo-main.png' alt='company logo' style='margin-top: 6px;'>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </di>
    </center>
</bod>
</html>
";
        }
    }
  
}
