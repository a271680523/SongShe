///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-12 18:24:39
//CreateBy 		唐翔
//Content       学生课程记录实体类
//////////////////////////////////////////////////////////////////
using System;
using Common;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 学生课程记录实体类
    /// </summary>
    public class StudentCourseRecordModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 课程老师ID
        /// </summary>
        public int CourseManagerID { get; set; }
        /// <summary>
        /// 课程记录表ID
        /// </summary>
        public int CourseRecordID { get; set; }
        /// <summary>
        /// 所属学生产品ID
        /// </summary>
        public int StudentProductID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 课程完成情况填写时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? CourseSuccessTime { get; set; }
        /// <summary>
        /// 课程完成情况填写时间是否超时
        /// </summary>
        public bool IsCourseSuccessTimeOut { get; set; }
        /// <summary>
        /// 学生课程状态ID
        /// </summary>
        public int StudentCourseStatus { get; set; }
        /// <summary>
        /// 课程状态中文名
        /// </summary>
        public string StudentCourseStatusChinaName { get; set; }
        /// <summary>
        /// 是否计算课时
        /// </summary>
        public bool IsEffective { get; set; }
        /// <summary>
        /// 课程状态英文名
        /// </summary>
        public string StudentCourseStatusEnglishName { get; set; }
        /// <summary>
        /// 下节课教材ID
        /// </summary>
        public int NowBookID { get; set; }
        /// <summary>
        /// 当前教材名称
        /// </summary>
        public string NowBookName { get; set; }
        /// <summary>
        /// 下节课教材ID
        /// </summary>
        public int NextBookID { get; set; }
        /// <summary>
        /// 下节课教材名称
        /// </summary>
        public string NextBookName { get; set; }
        /// <summary>
        /// 教材购买情况
        /// </summary>
        public string BookBuyInfo { get; set; }
        /// <summary>
        /// 本节课内容
        /// </summary>
        public string CourseContent { get; set; }
        /// <summary>
        /// 作业
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// 课堂问题
        /// </summary>
        public string CourseQuestion { get; set; }
        /// <summary>
        /// 重点内容
        /// </summary>
        public string CourseEmphasis { get; set; }
        /// <summary>
        /// 学生表现
        /// </summary>
        public string StudentPerformed { get; set; }
        /// <summary>
        /// 教师反馈
        /// </summary>
        public string CourseManagerFeedback { get; set; }
        /// <summary>
        /// 学生反馈
        /// </summary>
        public string StudentFeeback { get; set; }
        /// <summary>
        /// 下节课是否更换教材
        /// </summary>
        public bool IsChangeBook { get; set; }
        /// <summary>
        /// 更换教材原因
        /// </summary>
        public string ChangeBookCause { get; set; }
        /// <summary>
        /// 学生所处时区ID
        /// </summary>
        public int StudentTimeZoneID { get; set; }
        /// <summary>
        /// 任课老师所属时区ID
        /// </summary>
        public int CourseManagerTimeZoneID { get; set; }

        /// <summary>
        /// 学生反馈时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StudentFeedbackTime { get; set; }
        /// <summary>
        /// 学生评分课程
        /// </summary>
        public int StudentRateLesson { get; set; }
        /// <summary>
        /// 学生评分任课老师
        /// </summary>
        public int StudentRateCourseManager { get; set; }
        /// <summary>
        /// 排序编号(属于产品的课程)
        /// </summary>
        public int SortNumberByProduct { get; set; }

        /// <summary>
        /// 教案ID
        /// </summary>
        public int TeachingPlanId { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary>
        public int TeachingPlanNumber { get; set; }
        /// <summary>
        /// 教案名称
        /// </summary>
        public string TeachingPlanName { get; set; }
        /// <summary>
        /// 课程计划内容
        /// </summary>
        public string CoursePlanContent { get; set; }

        /// <summary>
        /// 学习目标
        /// </summary>
        public LearningInfoModel LearningTarget { get; set; } = new LearningInfoModel();
        /// <summary>
        /// 学习能力
        /// </summary>
        public LearningInfoModel LearningAbility { get; set; } = new LearningInfoModel();
        /// <summary>
        /// 学生背景
        /// </summary>
        public string StudentVitae { get; set; }
        /// <summary>
        /// 推荐教材
        /// </summary>
        public string RecommendBook { get; set; }
        /// <summary>
        /// 购买意向
        /// </summary>
        public string BuyIntention { get; set; }

    }
    /// <summary>
    /// 学习信息实体类
    /// </summary>
    public class LearningInfoModel
    {
        /// <summary>
        /// 听
        /// </summary>
        public string Listen { get; set; }
        /// <summary>
        /// 说
        /// </summary>
        public string Speak { get; set; }
        /// <summary>
        /// 读
        /// </summary>
        public string Read { get; set; }
        /// <summary>
        /// 写
        /// </summary>
        public string Write { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string Other { get; set; }
    }
    /// <summary>
    /// 学生课程记录视图实体类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class v_StudentCourseRecordModel
    {
        #region 原字段
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 课程老师ID
        /// </summary>
        public int CourseManagerID { get; set; }
        /// <summary>
        /// 课程记录表ID
        /// </summary>
        public int CourseRecordID { get; set; }
        /// <summary>
        /// 所属学生产品ID
        /// </summary>
        public int StudentProductID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public int CourseCount { get; set; }
        /// <summary>
        /// 课程完成情况填写时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? CourseSuccessTime { get; set; }
        /// <summary>
        /// 课程完成情况填写时间是否超时
        /// </summary>
        public bool IsCourseSuccessTimeOut { get; set; }
        /// <summary>
        /// 学生课程状态ID
        /// </summary>
        public int StudentCourseStatus { get; set; }
        /// <summary>
        /// 课程状态名(中文)
        /// </summary>
        public string StudentCourseStatusChinaName { get; set; }
        /// <summary>
        /// 课程状态名(英文)
        /// </summary>
        public string StudentCourseStatusEnglishName { get; set; }
        /// <summary>
        /// 是否计算课时
        /// </summary>
        public bool IsEffective { get; set; }
        /// <summary>
        /// 下节课教材ID
        /// </summary>
        public int NowBookID { get; set; }
        /// <summary>
        /// 当前教材名称
        /// </summary>
        public string NowBookName { get; set; }
        /// <summary>
        /// 下节课教材ID
        /// </summary>
        public int NextBookID { get; set; }
        /// <summary>
        /// 下节课教材名称
        /// </summary>
        public string NextBookName { get; set; }
        /// <summary>
        /// 教材购买情况
        /// </summary>
        public string BookBuyInfo { get; set; }
        /// <summary>
        /// 本节课内容
        /// </summary>
        public string CourseContent { get; set; }
        /// <summary>
        /// 作业
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// 课堂问题
        /// </summary>
        public string CourseQuestion { get; set; }
        /// <summary>
        /// 重点内容
        /// </summary>
        public string CourseEmphasis { get; set; }
        /// <summary>
        /// 学生表现
        /// </summary>
        public string StudentPerformed { get; set; }
        /// <summary>
        /// 教师反馈
        /// </summary>
        public string CourseManagerFeedback { get; set; }
        /// <summary>
        /// 家长/学生反馈
        /// </summary>
        public string StudentFeeback { get; set; }
        /// <summary>
        /// 下节课是否更换教材
        /// </summary>
        public bool IsChangeBook { get; set; }
        /// <summary>
        /// 更换教材原因
        /// </summary>
        public string ChangeBookCause { get; set; }
        /// <summary>
        /// 学生所处时区ID
        /// </summary>
        public int StudentTimeZoneID { get; set; }
        /// <summary>
        /// 任课老师所属时区ID
        /// </summary>
        public int CourseManagerTimeZoneID { get; set; }
        /// <summary>
        /// 学生反馈时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StudentFeedbackTime { get; set; }
        /// <summary>
        /// 学生评分课程
        /// </summary>
        public int StudentRateLesson { get; set; }
        /// <summary>
        /// 学生评分任课老师
        /// </summary>
        public int StudentRateCourseManager { get; set; }
        /// <summary>
        /// 排序编号(属于产品的课程)
        /// </summary>
        public int SortNumberByProduct { get; set; }
        /// <summary>
        /// 教案ID
        /// </summary>
        public int TeachingPlanId { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary>
        public int TeachingPlanNumber { get; set; }
        /// <summary>
        /// 教案名称
        /// </summary>
        public string TeachingPlanName { get; set; }
        /// <summary>
        /// 课程计划内容
        /// </summary>
        public string CoursePlanContent { get; set; }

        /// <summary>
        /// 学习目标
        /// </summary>
        public LearningInfoModel LearningTarget { get; set; } = new LearningInfoModel();
        /// <summary>
        /// 学习能力
        /// </summary>
        public LearningInfoModel LearningAbility { get; set; } = new LearningInfoModel();
        /// <summary>
        /// 学生背景
        /// </summary>
        public string StudentVitae { get; set; }
        /// <summary>
        /// 推荐教材
        /// </summary>
        public string RecommendBook { get; set; }
        /// <summary>
        /// 购买意向
        /// </summary>
        public string BuyIntention { get; set; }
        #endregion

        #region 扩展显示字段
        /// <summary>
        /// 任课老师姓名
        /// </summary>
        public string CourseManagerName { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentChinaName { get; set; }
        /// <summary>
        /// 学生账号
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 是否主管老师
        /// </summary>
        public bool IsSupervisor { get; set; }
        /// <summary>
        /// 学生所处时区
        /// </summary>
        public TimeZoneModel StudentTimeZone { get; set; }
        /// <summary>
        /// 学生上课时间
        /// </summary>
        public DateTime StudentStartTime => StudentTimeZone == null ? StartTime : TimeZoneInfo.ConvertTimeFromUtc(StartTime, TimeZoneInfo.FindSystemTimeZoneById(StudentTimeZone.TimeZoneInfoId));

        /// <summary>
        /// 任课老师所属时区
        /// </summary>
        public TimeZoneModel CourseManagerTimeZone { get; set; }
        /// <summary>
        /// 任课老师上课时间
        /// </summary>
        public DateTime CourseManagerStartTime => CourseManagerTimeZone == null ? StartTime : TimeZoneInfo.ConvertTimeFromUtc(StartTime, TimeZoneInfo.FindSystemTimeZoneById(StudentTimeZone.TimeZoneInfoId));
        /// <summary>
        /// 是否前三新课
        /// </summary>
        public bool IsNewStudentCourse { get; set; }
        /// <summary>
        /// 学生课程类型
        /// </summary>
        public Keys.StudentCourseType StudentCourseType { get; set; }
        #endregion
    }
}