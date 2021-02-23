using AutoMapper;
using FileapiCli.CommandHandlers;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;

namespace FileapiCli.Maps
{
    class OptionsToCommands : Profile
    {
        public OptionsToCommands()
        {
            ////options mapping. user on constructor map on cmd
            CreateMap<UploadOptions, UploadFileCmd>();
            CreateMap<SendToEthnoFilesOptions, SendFileToEthnoFilesCmd>();

            CreateMap<ProcessEnthofilesFileOptions, UploadOptions>();
            CreateMap<ProcessEnthofilesFileOptions, SendToEthnoFilesOptions>();

            CreateMap<SendEthnofilesOptions, UploadOptions>();

            //MassPayments
            CreateMap<MassPaymentOption, UploadOptions>();

            CreateMap<MassTransfersCreditOption, UploadOptions>();
        }
    }
}
