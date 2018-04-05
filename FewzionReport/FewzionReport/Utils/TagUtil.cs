using FewzionReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FewzionReport.Utils
{
    public static class TagUtil
    {
        public static List<string> GetTagsForPrinting(List<AppliedTag> appliedTags, List<Tag> tags)
        {
            if (appliedTags == null) throw new ArgumentNullException("appliedTags");
            if (tags == null) throw new ArgumentNullException("tags");

            List<string> html = new List<string>();

            foreach (AppliedTag appliedTag in appliedTags.Where(appliedTag => !string.IsNullOrWhiteSpace(appliedTag.Tag)))
            {
                string color = "FFFFFF";  // Default to white
                var icon = "";

                Tag tag = tags.SingleOrDefault(t => t.ShortCode == appliedTag.Tag);
                if (tag != null)
                {
                    // Insurance - don't crash on a bad tag colour
                    if (Regex.Match(tag.Color, "^([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$").Success)
                        color = tag.Color;

                    icon = tag.IconCode;
                }

                if (string.IsNullOrEmpty(icon))
                {
                    System.Drawing.Color contrast = ColorUtil.ContrastColor(System.Drawing.ColorTranslator.FromHtml("#" + color));
                    string contrastRGB = String.Format("{0}{1}{2}", contrast.R.ToString("X2"), contrast.G.ToString("X2"), contrast.B.ToString("X2"));
                    html.Add(String.Format("<span style=\"background-color:#{1}; color:#{2};\">&nbsp;{0}&nbsp;</span>", appliedTag.Tag.Replace(" ", "&nbsp;"), color, contrastRGB));
                }
                else
                {
                    html.Add("<i style=\"font-family: FontAwesome; font-style: normal; font-weight: normal; color: black\">&#x" + icon + ";</i>");
                }
            }

            return html;
        }
    }
}
