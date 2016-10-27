using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    [MetadataType(typeof(p_CollegeMetadata))]
    public partial class p_College
    {


    }
    public class p_CollegeMetadata
    {
        [Display(Name = "Collège d'enseignement")]
        public int id_College;
    }

}