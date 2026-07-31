using System;
using System.Collections.Generic;
using JoistCalculator.Core.Models;

namespace JoistCalculator.Core.Validation
{
    public static class InputValidator
    {
        public static List<string> Validate(JoistInput input)
        {
            var errors = new List<string>();

            if (input.Fc <= 0)
                errors.Add("مقاومت فشاری بتن ($f'_c$) باید بزرگتر از صفر باشد.");
            if (input.Fy <= 0)
                errors.Add("تنش تسلیم میلگرد طولی ($f_y$) باید بزرگتر از صفر باشد.");
            if (input.Fyt <= 0)
                errors.Add("تنش تسلیم میلگرد عرضی ($f_{yt}$) باید بزرگتر از صفر باشد.");
            if (input.SpanLength <= 0)
                errors.Add("طول دهانه تیرچه ($L$) باید بزرگتر از صفر باشد.");
            if (input.TotalHeight <= 0)
                errors.Add("ارتفاع کل تیرچه ($h$) باید بزرگتر از صفر باشد.");
            if (input.SlabThickness <= 0)
                errors.Add("ضخامت دال بتنی رویه باید بزرگتر از صفر باشد.");
            if (input.JoistSpacing <= 0)
                errors.Add("فاصله خالص تیرچه ها ($S$) باید بزرگتر از صفر باشد.");
            if (input.RibWidth <= 0)
                errors.Add("عرض تک تیرچه ($W$) باید بزرگتر از صفر باشد.");
            if (input.LiveLoad < 0)
                errors.Add("بار زنده نمی‌تواند منفی باشد.");
            if (input.SuperDeadLoad < 0)
                errors.Add("بار مرده کف سازی نمی‌تواند منفی باشد.");
            if (input.PointLoad < 0)
                errors.Add("بار متمرکز نمی‌تواند منفی باشد.");
            if (input.ShearRebarSpacing <= 0)
                errors.Add("فاصله میلگرد برشی باید بزرگتر از صفر باشد.");

            return errors;
        }
    }
}
