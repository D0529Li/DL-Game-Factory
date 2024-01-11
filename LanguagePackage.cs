using System.Windows;

namespace DL_Game_Project
{
    public enum LanguageChoices
    {
        English,
        Chinese
    }
    public class LanguagePackages
    {
        public static class EnglishPackage
        {
            public static string title = "Greedy Snake";
            public static string subtitle = "designed by David Li";
            public static string option1 = "New Game";
            public static string option2 = "Records";
            public static string option3 = "Exit";
            public static string querySpeed = "Choose speed: ";
            public static string queryName = "Enter your name: ";
            public static string snakeDies = "Snake dies due to ";
            public static string selfCollision = "collision with itself.";
            public static string collisionWithWall = "collision with wall.";
            public static string scoreDisplay = "Your score is: ";
            public static string confirm = "Confirm";
            public static string recordDisplay1 = "Current record is: ";
            public static string recordDisplay2 = " by ";
            public static string recordNoRecord = "No record found.\nPlay now to get the first record!";
            public static string speedSlow = "Slow";
            public static string speedMedium = "Medium";
            public static string speedFast = "Fast";
            public static string confirmWithNoName = "Please enter your name.";
            public static string confirmWithNoSpeed = "Please choose a speed.";
            public static string currentScore = "Current Score: ";
            public static string aboutGame = "About Greedy Snake";
            public static string aboutMe = "About Me";
            public static string aboutGameContent = "Your aim in Greedy Snake is to get as much candy as you can.\n" +
                "Make Sure don't collide with walls or yourself! \nPress up/down/left/right to adjust your direction. ";
            public static string aboutMeContent = "Yuchen Li, a second-year Software Engineering student in University of Waterloo, " +
                "designed this game using WPF(C# and XAML) during his summer time in 2019.";
            public static Thickness margin_Title = new Thickness(280, 30, 0, 0);
            public static Thickness margin_Subtitle = new Thickness(30, 90, 0, 0);
        }
        public static class ChinesePackage
        {
            public static string title = "贪吃蛇";
            public static string subtitle = "开发人: 李雨晨";
            public static string option1 = "新游戏";
            public static string option2 = "最高纪录";
            public static string option3 = "退出";
            public static string querySpeed = "选择速度：";
            public static string queryName = "请输出玩家名: ";
            public static string snakeDies = "贪吃蛇撞晕了，由于";
            public static string selfCollision = "和自己的碰撞。";
            public static string collisionWithWall = "撞上墙了。";
            public static string scoreDisplay = "你的得分是：";
            public static string confirm = "确认";
            public static string recordDisplay1 = "当前记录为：";
            public static string recordDisplay2 = "\n记录保持者：";
            public static string recordNoRecord = "没有找到记录，快来成为第一个记录保持者吧！";
            public static string speedSlow = "慢速";
            public static string speedMedium = "中速";
            public static string speedFast = "高速";
            public static string confirmWithNoName = "请输入玩家名。";
            public static string confirmWithNoSpeed = "请选择速度。";
            public static string currentScore = "当前得分: ";
            public static string aboutGame = "关于贪吃蛇";
            public static string aboutMe = "关于我";
            public static string aboutGameContent = "方向键控制小蛇的方向，你的目标是吃到不断出现的糖果。注意不要撞上墙和自己！";
            public static string aboutMeContent = "李雨晨，滑铁卢大学大二软件工程专业在读本科生\n在2019年夏天用WPF(C#和XAML)独立设计开发此游戏。";
            public static Thickness margin_Title = new Thickness(370, 30, 0, 0);
            public static Thickness margin_Subtitle = new Thickness(230, 90, 0, 0);
        }
        public static void CopyTo(LanguageChoices languageChoice, SnakeGame window)
        {
            if (languageChoice == LanguageChoices.Chinese)
            {
                window.StringTitle = ChinesePackage.title;
                window.StringSubtitle = ChinesePackage.subtitle;
                window.StringOption1 = ChinesePackage.option1;
                window.StringOption2 = ChinesePackage.option2;
                window.StringOption3 = ChinesePackage.option3;
                window.StringQuerySpeed = ChinesePackage.querySpeed;
                window.StringQueryName = ChinesePackage.queryName;
                window.StringSnakeDies = ChinesePackage.snakeDies;
                window.StringSelfCollision = ChinesePackage.selfCollision;
                window.StringCollisionWithWall = ChinesePackage.collisionWithWall;
                window.StringScoreDisplay = ChinesePackage.scoreDisplay;
                window.StringConfirm = ChinesePackage.confirm;
                window.StringRecordDisplay1 = ChinesePackage.recordDisplay1;
                window.StringRecordDisplay2 = ChinesePackage.recordDisplay2;
                window.StringRecordNoRecord = ChinesePackage.recordNoRecord;
                window.StringSpeedSlow = ChinesePackage.speedSlow;
                window.StringSpeedMedium = ChinesePackage.speedMedium;
                window.StringSpeedFast = ChinesePackage.speedFast;
                window.StringConfirmWithNoName = ChinesePackage.confirmWithNoName;
                window.StringConfirmWithNoSpeed = ChinesePackage.confirmWithNoSpeed;
                window.StringCurrentScore = ChinesePackage.currentScore;
                window.StringAboutGame = ChinesePackage.aboutGame;
                window.StringAboutMe = ChinesePackage.aboutMe;
                window.Margin_Title = ChinesePackage.margin_Title;
                window.Margin_Subtitle = ChinesePackage.margin_Subtitle;
                window.ContentAboutGame = ChinesePackage.aboutGameContent;
                window.ContentAboutMe = ChinesePackage.aboutMeContent;
            }
            else if (languageChoice == LanguageChoices.English)
            {
                window.StringTitle = EnglishPackage.title;
                window.StringSubtitle = EnglishPackage.subtitle;
                window.StringOption1 = EnglishPackage.option1;
                window.StringOption2 = EnglishPackage.option2;
                window.StringOption3 = EnglishPackage.option3;
                window.StringQuerySpeed = EnglishPackage.querySpeed;
                window.StringQueryName = EnglishPackage.queryName;
                window.StringSnakeDies = EnglishPackage.snakeDies;
                window.StringSelfCollision = EnglishPackage.selfCollision;
                window.StringCollisionWithWall = EnglishPackage.collisionWithWall;
                window.StringScoreDisplay = EnglishPackage.scoreDisplay;
                window.StringConfirm = EnglishPackage.confirm;
                window.StringRecordDisplay1 = EnglishPackage.recordDisplay1;
                window.StringRecordDisplay2 = EnglishPackage.recordDisplay2;
                window.StringRecordNoRecord = EnglishPackage.recordNoRecord;
                window.StringSpeedSlow = EnglishPackage.speedSlow;
                window.StringSpeedMedium = EnglishPackage.speedMedium;
                window.StringSpeedFast = EnglishPackage.speedFast;
                window.StringConfirmWithNoName = EnglishPackage.confirmWithNoName;
                window.StringConfirmWithNoSpeed = EnglishPackage.confirmWithNoSpeed;
                window.StringCurrentScore = EnglishPackage.currentScore;
                window.StringAboutGame = EnglishPackage.aboutGame;
                window.StringAboutMe = EnglishPackage.aboutMe;
                window.Margin_Title = EnglishPackage.margin_Title;
                window.Margin_Subtitle = EnglishPackage.margin_Subtitle;
                window.ContentAboutGame = EnglishPackage.aboutGameContent;
                window.ContentAboutMe = EnglishPackage.aboutMeContent;
            }
        }
    }
}
