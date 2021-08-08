using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Model
{
   public enum MemberTypes
    {
        Kierownik,
        ProjectMenager,
        TeamLeader,
        Engineer

    }

    public enum TypesOfResponsibility
    {
        Responsible,
        Approver,
        Consulted,
        Informed
    }

   public class TeamMember
    {
        public string Name;
        public  MemberTypes TypeOfMember;
        public  string FunctionInProject;
        public ObservableCollection<TypesOfResponsibility> Responsibilities;

    }
}
