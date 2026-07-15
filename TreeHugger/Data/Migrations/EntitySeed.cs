using FluentMigrator;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Migrations
{
    [Migration(2)]
    public class EntitySeed : Migration
    {
        public override void Up()
        {
            Create.Table(nameof(AchievementEntity))
                .WithColumn(nameof(AchievementEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(AchievementEntity.EducatorId)).AsGuid().Nullable()
                .WithColumn(nameof(AchievementEntity.Name)).AsString().Nullable()
                .WithColumn(nameof(AchievementEntity.Description)).AsString().Nullable()
                .WithColumn(nameof(AchievementEntity.Requirement)).AsString().Nullable()
                .WithColumn(nameof(AchievementEntity.Icon)).AsString().Nullable();

            Create.Table(nameof(ChoiceEntity))
                .WithColumn(nameof(ChoiceEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(ChoiceEntity.QuestionId)).AsGuid().Nullable()
                .WithColumn(nameof(ChoiceEntity.Text)).AsString().Nullable()
                .WithColumn(nameof(ChoiceEntity.Answer)).AsBoolean().Nullable();

            Create.Table(nameof(CommentEntity))
                .WithColumn(nameof(CommentEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(CommentEntity.LessonId)).AsGuid().Nullable()
                .WithColumn(nameof(CommentEntity.UserId)).AsString().Nullable()
                .WithColumn(nameof(CommentEntity.Content)).AsString().Nullable()
                .WithColumn(nameof(CommentEntity.Edited)).AsBoolean().Nullable()
                .WithColumn(nameof(CommentEntity.Pinned)).AsBoolean().Nullable()
                .WithColumn(nameof(CommentEntity.TimeStamp)).AsDateTime().Nullable();

            Create.Table(nameof(LessonEntity))
                .WithColumn(nameof(LessonEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(LessonEntity.EducatorId)).AsGuid().Nullable()
                .WithColumn(nameof(LessonEntity.Banner)).AsString().Nullable()
                .WithColumn(nameof(LessonEntity.Content)).AsString().Nullable()
                .WithColumn(nameof(LessonEntity.Description)).AsString().Nullable()
                .WithColumn(nameof(LessonEntity.TimeStamp)).AsDateTime().Nullable();

            Create.Table(nameof(ProfileEntity))
                .WithColumn(nameof(ProfileEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(ProfileEntity.UserId)).AsString().Nullable()
                .WithColumn(nameof(ProfileEntity.Avatar)).AsString().Nullable();

            Create.Table(nameof(QuestionEntity))
                .WithColumn(nameof(QuestionEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(QuestionEntity.QuizId)).AsGuid().Nullable()
                .WithColumn(nameof(QuestionEntity.Text)).AsString().Nullable()
                .WithColumn(nameof(QuestionEntity.Points)).AsDecimal().Nullable();

            Create.Table(nameof(QuizEntity))
                .WithColumn(nameof(QuizEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(QuizEntity.LessonId)).AsGuid().Nullable()
                .WithColumn(nameof(QuizEntity.Banner)).AsString().Nullable()
                .WithColumn(nameof(QuizEntity.Title)).AsString().Nullable()
                .WithColumn(nameof(QuizEntity.Score)).AsDecimal().Nullable();

            Create.Table(nameof(ReplyEntity))
                .WithColumn(nameof(ReplyEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(ReplyEntity.CommentId)).AsGuid().Nullable()
                .WithColumn(nameof(ReplyEntity.UserId)).AsString().Nullable()
                .WithColumn(nameof(ReplyEntity.Content)).AsString().Nullable()
                .WithColumn(nameof(ReplyEntity.Edited)).AsBoolean().Nullable()
                .WithColumn(nameof(ReplyEntity.Pinned)).AsBoolean().Nullable()
                .WithColumn(nameof(ReplyEntity.TimeStamp)).AsDateTime().Nullable();

            Create.Table(nameof(StudentAchievementEntity))
                .WithColumn(nameof(StudentAchievementEntity.Id)).AsGuid().PrimaryKey().Nullable()
                .WithColumn(nameof(StudentAchievementEntity.ProfileId)).AsGuid().Nullable()
                .WithColumn(nameof(StudentAchievementEntity.AchievementId)).AsGuid().Nullable()
                .WithColumn(nameof(StudentAchievementEntity.TimeStamp)).AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(nameof(StudentAchievementEntity));
            Delete.Table(nameof(ReplyEntity));
            Delete.Table(nameof(QuizEntity));
            Delete.Table(nameof(QuestionEntity));
            Delete.Table(nameof(ProfileEntity));
            Delete.Table(nameof(LessonEntity));
            Delete.Table(nameof(CommentEntity));
            Delete.Table(nameof(ChoiceEntity));
            Delete.Table(nameof(AchievementEntity));
        }
    }
} 