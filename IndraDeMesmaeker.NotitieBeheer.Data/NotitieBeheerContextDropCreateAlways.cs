using System.Data.Entity;

namespace IndraDeMesmaeker.NotitieBeheer.Data
{
    public class NotitieBeheerContextDropCreateAlways : DropCreateDatabaseAlways<NotitieBeheerContext>
    {
        protected override void Seed(NotitieBeheerContext context)
        {
            //Text is kept in RTF format so it can be stylized
            Category testCat00 = new Category("Home", "#FFADD8E6");
            testCat00.Notes.Add(new Note("TestNote00", "Jimmy Carr", "{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Invite guests for 'The big fat quiz of the year 2019'.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}"));
            testCat00.Notes.Add(new Note("TestNote01", "Dara o'briain", "{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Prepare introductions for }{\\b\\ltrch 'Mock the week'.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}"));
            testCat00.Notes.Add(new Note("TestNote02", "Alex Agnew", "{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Alter jokes for next performance.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}"));
            context.Categories.Add(testCat00);

            Category testCat01 = new Category("School", "#FF90EE90", "All notes for Odisee");
            testCat01.Notes.Add(new Note("TestNote03", "indra", "{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Presentation on 2/12 - don't forget powerpoint.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}"));
            testCat01.Notes.Add(new Note("TestNote04", "Jan", "{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch No classes 3/12 - turn off alarm.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}"));
            context.Categories.Add(testCat01);

            context.SaveChanges();
        }
    }
}
