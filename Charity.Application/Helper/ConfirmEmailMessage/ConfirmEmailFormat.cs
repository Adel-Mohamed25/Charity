namespace Charity.Application.Helper.ConfirmEmailMessage
{
    public static class ConfirmEmailFormat
    {
        private const string logoUrl = "https://yourwebsite.com/logo.png";
        private const string charityName = "جمعية يد العطاء";
        public static string GenerateHtmlContent(string message, string color, string extraContent = "", string title = "تأكيد البريد الإلكتروني")
        {
            return $@"
            <!DOCTYPE html>
            <html lang='ar'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{title}</title>
                <style>
                    body {{
                        font-family: 'Tajawal', Arial, sans-serif;
                        text-align: center;
                        padding: 50px;
                        background-color: #f4f4f4;
                        direction: rtl;
                        margin: 0;
                    }}
                    .container {{
                        background: white;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        max-width: 450px;
                        margin: auto;
                    }}
                    h2 {{
                        color: {color};
                        margin-top: 0;
                    }}
                    p {{
                        color: #333;
                        font-size: 18px;
                    }}
                    .logo {{
                        width: 100px;
                        margin-bottom: 10px;
                    }}
                    .button {{
                        text-decoration: none;
                        color: white;
                        background: #3498db;
                        padding: 10px 20px;
                        border-radius: 5px;
                        display: inline-block;
                        margin-top: 20px;
                        font-size: 18px;
                        transition: background 0.3s ease;
                    }}
                    .button:hover {{
                        background: #2980b9;
                    }}
                    @media (max-width: 480px) {{
                        .container {{
                            padding: 15px;
                        }}
                        .button {{
                            font-size: 16px;
                            padding: 8px 16px;
                        }}
                    }}
                </style>
                <link href='https://fonts.googleapis.com/css2?family=Tajawal:wght@400;700&display=swap' rel='stylesheet'>
            </head>
            <body>
                <div class='container'>
                    <img src='{logoUrl}' alt='شعار {charityName}' class='logo'>
                    <h2>{message}</h2>
                    <p>{charityName}</p>
                    {extraContent}
                </div>
            </body>
            </html>";
        }
    }

}
