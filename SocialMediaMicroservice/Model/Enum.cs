using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public enum EVideoLevel
    {
        Undefined = 0, 
        Basic = 1,
        Bronze = 2,
        Sliver = 3,
        Gold = 4
    }
    public enum EVideoPrivacy
    {
        Undefined = 0,
        Standard = 1,
        CC = 2,
    }
    public enum EVideoContentType
    {
        Undefined = 0,
        [Description("For all")]
        ForAll = 1,
        [Description("For adult")]
        ForAdult = 2,
        [Description("For kid")]
        ForKid = 3,
    }
}
