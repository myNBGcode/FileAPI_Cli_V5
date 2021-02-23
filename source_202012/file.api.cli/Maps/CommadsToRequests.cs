using AutoMapper;
using file.types;
using FileapiCli.CommandHandlers;
using FileapiCli.Commands;

namespace FileapiCli.Maps
{
    public class CommandsToRequests : Profile
    {
        public CommandsToRequests()
        {

            CreateMap<InitiateUploadCmd, UploadInitiationRequest>()
                .ForPath(s => s.Requester.Id, dest => dest.MapFrom(s => s.UserInfo.UserName))
                .ForPath(s => s.Requester.Registry, dest => dest.MapFrom(s => s.UserInfo.Registry))
                .ForPath(s => s.Subject.Id, dest => dest.MapFrom(s => s.UserInfo.SubjectUser))
                .ForPath(s => s.Subject.Registry, dest => dest.MapFrom(s => s.UserInfo.SubjectRegistry));


            CreateMap<UploadFileCmd, UploadInitiationRequest>()
                .ForPath(s => s.Requester.Id, dest => dest.MapFrom(s => s.UserInfo.UserName))
                .ForPath(s => s.Requester.Registry, dest => dest.MapFrom(s => s.UserInfo.Registry))
                .ForPath(s => s.Subject.Id, dest => dest.MapFrom(s => s.UserInfo.SubjectUser))
                .ForPath(s => s.Subject.Registry, dest => dest.MapFrom(s => s.UserInfo.SubjectRegistry));
         

            CreateMap<AddUserTagsCmd, UpdateFileRequest>()
                .ForPath(s => s.Requester.Id, dest => dest.MapFrom(s => s.UserInfo.UserName))
                .ForPath(s => s.Requester.Registry, dest => dest.MapFrom(s => s.UserInfo.Registry))
                .ForPath(s => s.Subject.Id, dest => dest.MapFrom(s => s.UserInfo.SubjectUser))
                .ForPath(s => s.Subject.Registry, dest => dest.MapFrom(s => s.UserInfo.SubjectRegistry));

            CreateMap<RemoveUserTagsCmd, UpdateFileRequest>()
                .ForPath(s => s.Requester.Id, dest => dest.MapFrom(s => s.UserInfo.UserName))
                .ForPath(s => s.Requester.Registry, dest => dest.MapFrom(s => s.UserInfo.Registry))
                .ForPath(s => s.Subject.Id, dest => dest.MapFrom(s => s.UserInfo.SubjectUser))
                .ForPath(s => s.Subject.Registry, dest => dest.MapFrom(s => s.UserInfo.SubjectRegistry));

            CreateMap<UploadFileCmd, UploadRequest>()
                .ForPath(s => s.Requester.Id, dest => dest.MapFrom(s => s.UserInfo.UserName))
                .ForPath(s => s.Requester.Registry, dest => dest.MapFrom(s => s.UserInfo.Registry))
                .ForPath(s => s.Subject.Id, dest => dest.MapFrom(s => s.UserInfo.SubjectUser))
                .ForPath(s => s.Subject.Registry, dest => dest.MapFrom(s => s.UserInfo.SubjectRegistry));
        }
    }
}
