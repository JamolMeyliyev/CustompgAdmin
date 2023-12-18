using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.DataAccess.Entities;


public  enum EnumDataType
{

    SERIAL,
    BIGSERIAL,
    INTEGER,
    BOOLEAN,
    BIGINT,
    CHARACTER,
    DATE,
    TIMESTAMP,
    TEXT,
    UUID,
    VARCHAR,
    CHAR
   
}

