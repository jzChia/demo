using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Demo
{
    public class CurrentUser:IPrincipal
    {
        private string[] m_roles;

        private int m_query_grader;

        private int m_get_grader;

        private IIdentity m_identity;

        public CurrentUser(IIdentity identity, string[] roles)
        {
            if (identity == null)
                throw new ArgumentNullException( "identity","Argument must not be null");

            m_identity = identity;
            m_roles = roles;
            m_query_grader = -1;
            m_get_grader = -1;

        }

        public CurrentUser(IIdentity identity, string[] roles, int queryGrader, int getGrader)
        {
            if (identity == null)
                throw new ArgumentNullException("identity","Argument must not be null");

            m_identity = identity;
            m_roles = roles;
            m_query_grader = queryGrader;
            m_get_grader = getGrader;

        }

        public bool IsInRole(string roleName)
        {
            return m_roles != null && Array.IndexOf(m_roles, roleName) >= 0;
        }

        public IIdentity Identity
        {
            get { return m_identity; }
        }


        public int GetQueryRight()
        {
            return m_query_grader;
        }

        public int GetGetRight()
        {
            return m_get_grader;
        }
    }
}
