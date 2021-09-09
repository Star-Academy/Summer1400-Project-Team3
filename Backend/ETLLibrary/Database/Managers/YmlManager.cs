﻿using System.IO;
using ETLLibrary.Database.Gataways;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;

namespace ETLLibrary.Database.Managers
{
    public class YmlManager:IYmlManager
    {

        private IPipelineGateway _pipelineGateway;

        public YmlManager(EtlContext context)
        {
            _pipelineGateway = new PipelineGateway(context);
        }

        public void SaveYml(Stream openReadStream, string name, string username, long fileLength)
        {
            var content = FormFileReader.Read(openReadStream, fileLength);
            var json = YmlToJsonConvertor.Convert(content);
            _pipelineGateway.AddPipeline(username, name, json);
        }
    }
}