﻿#region API 참조
using System.Collections.Generic;
#endregion

namespace Hanyang.Model
{
    public class Article
    {
        #region 변수
        public int Id { get; set; } // 글 번호
        public string Title { get; set; } // 글 제목
        public string Contents { get; set; } // 글 내용
        public string Info { get; set; } // 작성자, 작성 날짜 (ㅇㅇㅇ | 2020.06.23)
        public List<Attachment> Attachments { get; set; } // 첨부파일
        #endregion
    }
}
