using AutoMapper;
using FileapiCli.ConfigOptions;
using System;
using System.Collections;
using System.Linq;

namespace FileapiCli.Maps
{
    public class DefaultOptionsProfile : Profile
    {
        public DefaultOptionsProfile()
        {
            //replace destination (commandline options) with options from destination:defaultOptions if null or empty
            CreateMap<UploadOptions, UploadOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<DownloadOptions, DownloadOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<RemoveUserTagOptions, RemoveUserTagOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));
            CreateMap<DefaultOptions, DefaultOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<AddUserTagsOptions, AddUserTagsOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<ProcessEnthofilesFileOptions, ProcessEnthofilesFileOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<SendToEthnoFilesOptions, SendToEthnoFilesOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<MassPaymentOption, MassPaymentOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));
            CreateMap<MassPaymentOutcomeOption, MassPaymentOutcomeOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));
            CreateMap<RequestPaymentStatusOption, RequestPaymentStatusOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<RetrievePaymentStatusOption, RetrievePaymentStatusOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));


            CreateMap<SampleMassPaymentOption, SampleMassPaymentOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));


            CreateMap<SampleMassTransfersOption, SampleMassTransfersOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<MassTransfersCreditOutcomeOption, MassTransfersCreditOutcomeOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<MassTransfersCreditOption, MassTransfersCreditOption>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<DownloadFilesIncomingOptions, DownloadFilesIncomingOptions>().ForAllMembers(o =>
                o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                         || (destMember.GetType().Name == "String[]" &&
                                                                             ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <DownloadFilesOutgoingOptions, DownloadFilesOutgoingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveCustomerApplicationsIncomingOptions, RetrieveCustomerApplicationsIncomingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveCustomerApplicationsOutgoingOptions, RetrieveCustomerApplicationsOutgoingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFileIncomingOptions, RetrieveFileIncomingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFileOutgoingOptions, RetrieveFileOutgoingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFilesIncomingHistoricOptions, RetrieveFilesIncomingHistoricOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFilesIncomingOptions, RetrieveFilesIncomingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFilesOutgoingHistoricOptions, RetrieveFilesOutgoingHistoricOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap <RetrieveFilesOutgoingOptions, RetrieveFilesOutgoingOptions> ().ForAllMembers(o =>
                  o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                           || (destMember.GetType().Name == "String[]" &&
                                                                               ((IEnumerable)destMember).Cast<object>().Any() == false)));

            CreateMap<SendEthnofilesOptions, SendEthnofilesOptions>().ForAllMembers(o =>
               o.Condition((src, dest, srcMember, destMember, value) => (destMember == null)
                                                                        || (destMember.GetType().Name == "String[]" &&
                                                                            ((IEnumerable)destMember).Cast<object>().Any() == false)));
        }
    }
}
