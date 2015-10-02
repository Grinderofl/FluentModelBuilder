using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ModelBuilderSample;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace ModelBuilderSample.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    partial class FirstProjectDbContext
    {
        public override string Id
        {
            get { return "20151002131107_FirstProjectDbContext"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("ModelBuilderSample.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });
        }
    }
}
