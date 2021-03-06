// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata.Builders
{
    /// <summary>
    ///     <para>
    ///         Provides a simple API for configuring a one-to-many relationship.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from methods when using the <see cref="ModelBuilder" /> API
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    /// <typeparam name="TEntity"> The entity type to be configured. </typeparam>
    /// <typeparam name="TRelatedEntity"> The entity type that this relationship targets. </typeparam>
    public class OneToManyBuilder<TEntity, TRelatedEntity> : OneToManyBuilder
        where TEntity : class
    {
        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="OneToManyBuilder{TEntity, TRelatedEntity}" /> class.
        ///     </para>
        ///     <para>
        ///         Instances of this class are returned from methods when using the <see cref="ModelBuilder" /> API
        ///         and it is not designed to be directly constructed in your application code.
        ///     </para>
        /// </summary>
        /// <param name="builder"> The internal builder being used to configure this relationship. </param>
        public OneToManyBuilder([NotNull] InternalRelationshipBuilder builder)
            : base(builder)
        {
        }

        /// <summary>
        ///     <para>
        ///         Configures the property(s) to use as the foreign key for this relationship.
        ///     </para>
        ///     <para>
        ///         If <see cref="ReferencedKey(Expression{Func{TEntity, object}})" /> is not specified, then an
        ///         attempt will be made to match the data type and order of foreign key properties against the primary
        ///         key of
        ///         the principal entity type. If they do not match, new shadow state properties that form a unique
        ///         index
        ///         will be added to the principal entity type to serve as the reference key.
        ///         A shadow state property is one that does not have a corresponding property in the entity class. The
        ///         current value for the property is stored in the <see cref="ChangeTracker" /> rather than being
        ///         stored in instances of the entity class.
        ///     </para>
        /// </summary>
        /// <param name="foreignKeyExpression">
        ///     <para>
        ///         A lambda expression representing the foreign key property(s) (<c>t => t.Id1</c>).
        ///     </para>
        ///     <para>
        ///         If the foreign key is made up of multiple properties then specify an anonymous type including the
        ///         properties (<c>t => new { t.Id1, t.Id2 }</c>). The order specified should match the order of
        ///         corresponding keys in <see cref="ReferencedKey(Expression{Func{TEntity, object}})" />.
        ///     </para>
        /// </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public virtual OneToManyBuilder<TEntity, TRelatedEntity> ForeignKey(
            [NotNull] Expression<Func<TRelatedEntity, object>> foreignKeyExpression)
        {
            Check.NotNull(foreignKeyExpression, nameof(foreignKeyExpression));

            return new OneToManyBuilder<TEntity, TRelatedEntity>(Builder.ForeignKey(foreignKeyExpression.GetPropertyAccessList(), ConfigurationSource.Explicit));
        }

        /// <summary>
        ///     Configures the unique property(s) that this relationship targets. Typically you would only call this
        ///     method if you want to use a property(s) other than the primary key as the referenced property(s). If
        ///     the specified property(s) is not already a unique index (or the primary key) then a new unique index
        ///     will be introduced.
        /// </summary>
        /// <param name="keyExpression">
        ///     <para>
        ///         A lambda expression representing the reference key property(s) (<c>t => t.Id</c>).
        ///     </para>
        ///     <para>
        ///         If the referenced key is made up of multiple properties then specify an anonymous type including
        ///         the properties (<c>t => new { t.Id1, t.Id2 }</c>).
        ///     </para>
        /// </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public virtual OneToManyBuilder<TEntity, TRelatedEntity> ReferencedKey(
            [NotNull] Expression<Func<TEntity, object>> keyExpression)
        {
            Check.NotNull(keyExpression, nameof(keyExpression));

            return new OneToManyBuilder<TEntity, TRelatedEntity>(Builder.ReferencedKey(keyExpression.GetPropertyAccessList(), ConfigurationSource.Explicit));
        }

        /// <summary>
        ///     Adds or updates an annotation on the relationship. If an annotation with the key specified in
        ///     <paramref name="annotation" /> already exists it's value will be updated.
        /// </summary>
        /// <param name="annotation"> The key of the annotation to be added or updated. </param>
        /// <param name="value"> The value to be stored in the annotation. </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public new virtual OneToManyBuilder<TEntity, TRelatedEntity> Annotation([NotNull] string annotation, [NotNull] string value)
        {
            Check.NotEmpty(annotation, nameof(annotation));
            Check.NotEmpty(value, nameof(value));

            return (OneToManyBuilder<TEntity, TRelatedEntity>)base.Annotation(annotation, value);
        }

        /// <summary>
        ///     <para>
        ///         Configures the property(s) to use as the foreign key for this relationship.
        ///     </para>
        ///     <para>
        ///         If the specified property name(s) do not exist on the entity type then a new shadow state
        ///         property(s) will be added to serve as the foreign key. A shadow state property is one
        ///         that does not have a corresponding property in the entity class. The current value for the
        ///         property is stored in the <see cref="ChangeTracker" /> rather than being stored in instances
        ///         of the entity class.
        ///     </para>
        ///     <para>
        ///         If <see cref="ReferencedKey(string[])" /> is not specified, then an attempt will be made to match
        ///         the data type and order of foreign key properties against the primary key of the principal
        ///         entity type. If they do not match, new shadow state properties that form a unique index will be
        ///         added to the principal entity type to serve as the reference key.
        ///     </para>
        /// </summary>
        /// <param name="foreignKeyPropertyNames">
        ///     The name(s) of the foreign key property(s).
        /// </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public new virtual OneToManyBuilder<TEntity, TRelatedEntity> ForeignKey([NotNull] params string[] foreignKeyPropertyNames)
        {
            Check.NotNull(foreignKeyPropertyNames, nameof(foreignKeyPropertyNames));

            return new OneToManyBuilder<TEntity, TRelatedEntity>(Builder.ForeignKey(foreignKeyPropertyNames, ConfigurationSource.Explicit));
        }

        /// <summary>
        ///     Configures the unique property(s) that this relationship targets. Typically you would only call this
        ///     method if you want to use a property(s) other than the primary key as the referenced property(s). If
        ///     the specified property(s) is not already a unique index (or the primary key) then a new unique index
        ///     will be introduced.
        /// </summary>
        /// <param name="keyPropertyNames"> The name(s) of the reference key property(s). </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public new virtual OneToManyBuilder<TEntity, TRelatedEntity> ReferencedKey([NotNull] params string[] keyPropertyNames)
        {
            Check.NotNull(keyPropertyNames, nameof(keyPropertyNames));

            return new OneToManyBuilder<TEntity, TRelatedEntity>(Builder.ReferencedKey(keyPropertyNames, ConfigurationSource.Explicit));
        }

        /// <summary>
        ///     Configures whether this is a required relationship (i.e. whether the foreign key property(s) can
        ///     be assigned null).
        /// </summary>
        /// <param name="required"> A value indicating whether this is a required relationship. </param>
        /// <returns> The same builder instance so that multiple configuration calls can be chained. </returns>
        public new virtual OneToManyBuilder<TEntity, TRelatedEntity> Required(bool required = true)
            => new OneToManyBuilder<TEntity, TRelatedEntity>(Builder.Required(required, ConfigurationSource.Explicit));
    }
}
