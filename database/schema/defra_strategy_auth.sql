-- custom type to contain the CPH reference
create type cph_type as
(
    county  varchar(2),
    parish  varchar(3),
    holding varchar(8)
);

alter type cph_type owner to postgres;

-- users table
create table users
(
    user_entra_id    uuid                     default gen_random_uuid() not null
        constraint users_pk
            primary key,
    email_address    varchar(256)                                       not null,
    is_active        boolean                  default true              not null,
    created_datetime timestamp with time zone default now()             not null,
    deleted_datetime timestamp with time zone
);

alter table users
    owner to postgres;

create unique index users_email_address_active_uindex
    on users (email_address, is_active);

create index users_email_address_index
    on users (email_address);

--- cphs table
create table cphs
(
    cph_id           uuid                     default gen_random_uuid() not null
        constraint pch_pk
            primary key,
    reference        cph_type                                           not null,
    is_active        boolean                  default true              not null,
    created_datetime timestamp with time zone default now()             not null,
    deleted_datetime timestamp with time zone
);

alter table cphs
    owner to postgres;

create unique index pch_reference_is_active_uindex
    on cphs (reference, is_active);

--- Mapping table between users and cphs
create table user_cph_mapping
(
    user_entra_id    uuid                                   not null
        constraint user_cph_mapping_users_user_id_fk
            references users,
    cph_id           uuid                                   not null
        constraint user_cph_mapping_cphs_cph_id_fk
            references cphs,
    is_active        boolean                  default true  not null,
    created_datetime timestamp with time zone default now() not null,
    deleted_datetime timestamp with time zone,
    constraint user_cph_mapping_pk
        primary key (user_entra_id, cph_id)
);

alter table user_cph_mapping
    owner to postgres;

create index user_cph_mapping_cph_id_index
    on user_cph_mapping (cph_id);

create index user_cph_mapping_user_id_index
    on user_cph_mapping (user_entra_id);


